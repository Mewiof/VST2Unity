using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace VST2Unity {

	public static class LibInterface {

		private const string dllName = "VST2Unity";

		[DllImport(dllName, EntryPoint = "initHost", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int InitHost();

		[DllImport(dllName, EntryPoint = "loadVST", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int LoadVST(string path, float sampleRate, int blockSize, bool isEffect);

		[DllImport(dllName, EntryPoint = "getNumParams", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int GetNumParams(int vstIndex);

		[DllImport(dllName, EntryPoint = "getParamName", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr GetParamName(int vstIndex, int paramIndex);

		[DllImport(dllName, EntryPoint = "setParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetParam(int vstIndex, int paramIndex, float value);

		[DllImport(dllName, EntryPoint = "getParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern float GetParam(int vstIndex, int paramIndex);

		[DllImport(dllName, EntryPoint = "processAudio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern void ProcessAudio(int vstIndex, [In, Out] float[] aData);

		[DllImport(dllName, EntryPoint = "sendNote", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SendNote(int vstIndex, int noteNumber, bool on);

		[DllImport(dllName, EntryPoint = "clearVSTs", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern void ClearVSTs();
	}

	public class Host : MonoBehaviour {

		public static float sampleRate;
		public static int blockSize;

		private void Awake() {
			LibInterface.InitHost();
			AudioSettings.GetDSPBufferSize(out blockSize, out _);
			sampleRate = AudioSettings.outputSampleRate;
		}

		private void OnApplicationQuit() {
			LibInterface.ClearVSTs();
		}
	}
}
