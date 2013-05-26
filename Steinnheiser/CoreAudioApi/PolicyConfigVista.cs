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
using CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CoreAudioApi {

	// Marked as internal, since on its own its no good
	[ComImport, Guid ("294935CE-F637-4E7C-A41B-AB255460B862")]
	internal class _PolicyConfigVista {
	}

	public class PolicyConfigVista {
		#region Variables
		private IPolicyConfigVista _RealPolicyConfigVista = new _PolicyConfigVista () as IPolicyConfigVista;

		#endregion

		#region Constructor
		//internal PolicyConfigVista (IPolicyConfigVista realPolicyConfigVista) {
		//	_RealPolicyConfigVista = realPolicyConfigVista;
		//}
		internal PolicyConfigVista () {
		}
		#endregion

		#region Methods
		public void SetDefaultEndpoint (string wszDeviceId, ERole eRole) {
			Marshal.ThrowExceptionForHR (_RealPolicyConfigVista.SetDefaultEndpoint (wszDeviceId, eRole));
		}
		#endregion

	}

}
