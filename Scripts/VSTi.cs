using UnityEngine;

namespace VST2Unity {

	public class VSTi : VSTBase {

		private void OnAudioFilterRead(float[] data, int _) {
			if (_vstIndex < 0) {
				return;
			}

			LibInterface.ProcessAudio(_vstIndex, data);
		}

		private void HandleKeyInput(int noteNumber, KeyCode key) {
			if (Input.GetKeyDown(key)) {
				LibInterface.SendNote(_vstIndex, noteNumber, true);
			}
			if (Input.GetKeyUp(key)) {
				LibInterface.SendNote(_vstIndex, noteNumber, false);
			}
		}

		private void Update() {
			HandleKeyInput(60, KeyCode.A);
			HandleKeyInput(62, KeyCode.S);
			HandleKeyInput(64, KeyCode.D);
			HandleKeyInput(65, KeyCode.F);
			HandleKeyInput(67, KeyCode.G);
			HandleKeyInput(69, KeyCode.H);
			HandleKeyInput(71, KeyCode.J);
			HandleKeyInput(72, KeyCode.K);
			HandleKeyInput(74, KeyCode.L);
		}
	}
}
