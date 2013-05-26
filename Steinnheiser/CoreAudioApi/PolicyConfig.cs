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
	[ComImport, Guid ("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
	internal class _PolicyConfig {
	}

	public class PolicyConfig {
		#region Variables
		private IPolicyConfigVista _RealPolicyConfigVista = new _PolicyConfigVista () as IPolicyConfigVista;

		#endregion

		#region Constructor
		//internal PolicyConfigVista (IPolicyConfigVista realPolicyConfigVista) {
		//	_RealPolicyConfigVista = realPolicyConfigVista;
		//}
		internal PolicyConfig () {
		}
		#endregion

		#region Methods
		public void SetDefaultEndpoint (string wszDeviceId, ERole eRole) {
			Marshal.ThrowExceptionForHR (_RealPolicyConfigVista.SetDefaultEndpoint (wszDeviceId, eRole));
		}
		#endregion

	}

}
