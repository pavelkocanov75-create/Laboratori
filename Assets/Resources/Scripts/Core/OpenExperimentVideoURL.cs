using System.Runtime.InteropServices;
using ARLaboratory.Scriptable_Object;
using UnityEngine;

namespace ARLaboratory.Core
{
    public class OpenExperimentVideoURL : MonoBehaviour
    {
        [SerializeField] private ExperimentManager _experimentManager;
        public void Open()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        openWindow(_experimentManager.CurrentExperiment.VideoUrl);
#endif
        }

        [DllImport("__Internal")]
        private static extern void openWindow(string url);
    }
}
