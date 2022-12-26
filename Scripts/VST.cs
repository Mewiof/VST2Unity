namespace VST2Unity {

	public class VST : VSTBase {

		protected override void Start() {
			_isEffect = true;
			base.Start();
		}

		private void OnAudioFilterRead(float[] data, int _) {
			if (_vstIndex < 0) {
				return;
			}

			LibInterface.ProcessAudio(_vstIndex, data);
		}
	}
}
