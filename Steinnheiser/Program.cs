using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using CoreAudioApi;
using CommandLine;
using CommandLine.Text;
using System.Runtime.InteropServices;
using iTunesLib;

// Debug/Trace - http://stackoverflow.com/questions/9466838/writing-to-output-window-of-visual-studio
// Parsing arguments - http://stackoverflow.com/questions/491595/best-way-to-parse-command-line-arguments-in-c
// CoreAudioApi - http://www.codeproject.com/Articles/18520/Vista-Core-Audio-API-Master-Volume-Control
// USB - http://libusbdotnet.sourceforge.net/V2/Index.html

namespace Steinnheiser {

	class Options {
		[Option ('d', "default", Required =false, HelpText = "Returns default device.")]
		public bool defaultDevice { get; set; }

		[Option ('a', "all", Required =false, HelpText = "Returns all devices (disabled devices included).")]
		public bool allDevices { get; set; }

		[Option ('c', "comm", Required = false, HelpText = "Communication device vs. Multimedia.")]
		public bool comm { get; set; }

		[Option ('v', "verbose", Required =false, HelpText = "Print details.")]
		public bool verbose { get; set; }

		[Option ('f', "format", Required =false, HelpText = "Output format.")]
		public string szFormat { get; set; }

		[Option ('i', "itunes", Required = false, HelpText = "Start iTunes with Badlands.")]
		public bool iTunes { get; set; }

		[Option ('m', "mute", Required = false, HelpText = "Mute/Unmute iTunes.")]
		public bool iTunesMute { get; set; }

		[Option ('s', "switch", Required = false, HelpText = "Switch Cyrille devices.")]
		public bool bSwitch { get; set; }

		[ValueList(typeof(List<string>), MaximumElements = 1)]
		public IList<string> Items { get; set; }

		[HelpOption(HelpText = "Display this help screen.")]
		public string GetUsage () {
			return (HelpText.AutoBuild (this, current => HelpText.DefaultParsingErrorsHandler (this, current))) ;
		}

		public EDeviceState deviceState = EDeviceState.DEVICE_STATE_ACTIVE;
		public ERole deviceRole = ERole.eMultimedia;
		public string [] cyrilleDevices = new string [] { "{18fdf94e-1bd7-42e6-8adb-1814dcb2bfa7}", "{c3b22a7d-0f09-4d1e-89b2-b2faabe0235e}" };
	}

	class Program {

		#region DllImports
		[DllImport ("kernel32.dll")]
		static extern bool AttachConsole (int dwProcessId);
		private const int ATTACH_PARENT_PROCESS = -1;
		[DllImport ("kernel32.dll")]
		static extern bool FreeConsole ();
		#endregion

		static void Main (string [] args) {
			AttachConsole (ATTACH_PARENT_PROCESS);
			Debug.WriteLine (" ");
			Console.WriteLine (" ");

			var options =new Options () ;
			options.szFormat ="{0}" ;
			var parser =new CommandLine.Parser (with => with.HelpWriter =Console.Error) ;

			if ( !parser.ParseArgumentsStrict (args, options, () => Environment.Exit (-2)) )
				return ;

			if ( options.iTunes ) {
				iTunes_Start (options) ;
				return;
			}
			
			MMDeviceEnumerator deviceEnum = new MMDeviceEnumerator ();
			MMDevice defaultDevice = deviceEnum.GetDefaultAudioEndpoint (EDataFlow.eRender, options.deviceRole);

			if ( options.bSwitch ) {
				iTunes_Mute (options);
				bool b = defaultDevice.ID.Contains (options.cyrilleDevices [0]);
				MMDevice device = deviceEnum.GetDevice ("{0.0.0.00000000}." + options.cyrilleDevices [Convert.ToInt16 (b)]);
				SetDefaultEndpoint (options, device);
				return;
			}

			if ( options.defaultDevice ) {
				printDevice (options, defaultDevice);
				FreeConsole ();
				return;
			} else if ( options.allDevices && options.Items.Count == 0 ) {
				options.deviceState = EDeviceState.DEVICE_STATEMASK_ALL;
			}
			if ( options.comm )
				options.deviceRole = ERole.eCommunications;

			if ( options.Items.Count == 1 ) {
				MMDevice device = deviceEnum.GetDevice ("{0.0.0.00000000}." + options.Items [0]);
				SetDefaultEndpoint (options, device);
			} else {
				MMDeviceCollection audioEndPointsEnum = deviceEnum.EnumerateAudioEndPoints (EDataFlow.eRender, options.deviceState);
				int count = audioEndPointsEnum.Count;
				for ( int i = 0 ; i < count ; i++ ) {
					MMDevice device = audioEndPointsEnum [i];
					printDevice (options, device);
					if ( options.verbose )
						device.DebugProperties ();
				}
			}
			if ( options.iTunesMute )
				iTunes_Mute (options) ;

			FreeConsole ();
		}

		static void iTunes_Start (Options options) {
			try {
				iTunesAppClass itunes = new iTunesAppClass ();
				System.Threading.Thread.Sleep (1000);
				//if ( !itunes.CurrentStreamURL.Contains ("http") )
				if ( options.Items.Count == 1 )
					itunes.OpenURL (options.Items [0]);
				else
					itunes.OpenURL ("http:/65.49.77.146:9574"); // The Badlands
				// http://badlandscountry.com -> http://www.audiorealm.com
				itunes.Mute =false;
			} catch ( Exception e ) {
			}
		}

		static void iTunes_Mute (Options options) {
			try {
				iTunesAppClass itunes = new iTunesAppClass ();
				itunes.Mute = !itunes.Mute;
			} catch ( Exception e ) {
			}
		}

		static void SetDefaultEndpoint (Options options, MMDevice device) {
			PolicyConfigVista pcv = new PolicyConfigVista ();
			pcv.SetDefaultEndpoint (device.ID, ERole.eConsole);
		}

		static void printDevice (Options options, MMDevice device) {
			Debug.WriteLine (" ");
			Debug.WriteLine (
				options.szFormat,
				new Object [] {
					device.ID,
					device.FriendlyName,
					device.Description,
					device.InterfaceFriendlyName,
					device.State.ToString()
				}
			);
			Console.WriteLine (" ");
			Console.WriteLine (
				options.szFormat,
				new Object [] {
					device.ID,
					device.FriendlyName,
					device.Description,
					device.InterfaceFriendlyName,
					device.State.ToString()
				}
			);
		}

	}

}
