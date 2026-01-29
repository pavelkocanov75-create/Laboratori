using System.Runtime.InteropServices;
using UnityEngine;

namespace ARLaboratory.Core
{
    public class WebGLBrowserChecker : MonoBehaviour
    {
    
// #if UNITY_WEBGL
//         [DllImport("__Internal")]
//         static extern bool IsMobilePlatform();
    
//         private static bool IsMobileBrowser()
//         {
// #if UNITY_EDITOR
//             return true; // value to return in Play Mode (in the editor)
// #elif UNITY_WEBGL
//         return IsMobilePlatform(); // value based on the current browser
// #else
//         return false;
// #endif
//         }
// #endif
    
//         private void Awake()
//         {
//             if (IsMobileBrowser())
//                 return;
        
//             ShowBlockWarning();
//         }
    
//         private void ShowBlockWarning()
//         {
//             Canvas canvas = gameObject.GetComponent<Canvas>();
//             canvas.enabled = true;
        
//             Debug.LogError("This build is intended for mobile devices only. Please switch to a mobile device or build for a different platform.");
//         }
    }
}
