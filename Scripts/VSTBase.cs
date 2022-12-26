using UnityEngine;

namespace VST2Unity {

	public abstract class VSTBase : MonoBehaviour {

		[SerializeField] private string _DLLPath;

		protected bool _isEffect;
		protected int _vstIndex;

		[System.Serializable]
		public struct Parameter {
			[HideInInspector]
			public string name;
			[Range(.0f, 1f)]
			public float value;
		}

		[Space]
		[SerializeField] private Parameter[] _parameters;
#if UNITY_EDITOR
		private float[] _prevParameters;
#endif

		protected virtual void Start() {
			_vstIndex = LibInterface.LoadVST(_DLLPath, Host.sampleRate, Host.blockSize, _isEffect);
			if (_vstIndex == -1) {
				Debug.LogError(_vstIndex == -1);
				return;
			}

			int numParams = LibInterface.GetNumParams(_vstIndex);
			_parameters = new Parameter[numParams];
#if UNITY_EDITOR
			_prevParameters = new float[numParams];
#endif
			for (int i = 0; i < numParams; i++) {
				_parameters[i].value = LibInterface.GetParam(_vstIndex, i);
#if UNITY_EDITOR
				_prevParameters[i] = _parameters[i].value;
#endif
				_parameters[i].name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(LibInterface.GetParamName(_vstIndex, i));
			}
		}

		public void SetParam(int paramIndex, float value) {
			_parameters[paramIndex].value = value;
#if UNITY_EDITOR
			_prevParameters[paramIndex] = value;
#endif
			LibInterface.SetParam(_vstIndex, paramIndex, value);
		}

#if UNITY_EDITOR
		private void OnValidate() {
			if (_prevParameters != null) {
				for (int i = 0; i < _prevParameters.Length; i++) {
					if (_prevParameters[i] != _parameters[i].value) {
						SetParam(i, _parameters[i].value);
					}
				}
			}
		}
#endif
	}
}
