/*
  LICENSE
  -------
  Copyright (C) 2013 - Cyrille Fauvel

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreAudioApi.Interfaces {

	[Guid ("568b9108-44bf-40b4-9006-86afe5b5a620"),
	InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPolicyConfigVista {

		[PreserveSig]
		int GetMixFormat (string a, out IntPtr b);  // not available on Windows 7, use method from IPolicyConfig
		[PreserveSig]
		int GetDeviceFormat (string a, int b, out IntPtr c);
		[PreserveSig]
		int SetDeviceFormat (string a, IntPtr b, IntPtr c);
		[PreserveSig]
		int GetProcessingPeriod (string a, int b, out IntPtr c, out IntPtr d);  // not available on Windows 7, use method from IPolicyConfig
		[PreserveSig]
		int SetProcessingPeriod (string a, IntPtr b);  // not available on Windows 7, use method from IPolicyConfig
		[PreserveSig]
		int GetShareMode (string a, out IntPtr b);  // not available on Windows 7, use method from IPolicyConfig
		[PreserveSig]
		int SetShareMode (string a, IntPtr b);  // not available on Windows 7, use method from IPolicyConfig
		[PreserveSig]
		int GetPropertyValue (string a, ref PropertyKey key, out PropVariant b);
		[PreserveSig]
		int SetPropertyValue (string a, ref PropertyKey key, ref PropVariant b);
		[PreserveSig]
		int SetDefaultEndpoint (string wszDeviceId, ERole eRole);
		[PreserveSig]
		int SetEndpointVisibility (string wszDeviceId, int vis);  // not available on Windows 7, use method from IPolicyConfig

	}

}

