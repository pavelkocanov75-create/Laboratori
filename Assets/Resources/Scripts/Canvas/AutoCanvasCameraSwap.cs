using System;
using ARLaboratory.Core;
using UnityEngine;

namespace ARLaboratory.CanvasControl
{
    public class AutoCanvasCameraSwap : MonoBehaviour
    {
        [SerializeField] private Camera _zapparCamera;
        [SerializeField] private Camera _regularCamera;

        private Canvas _canvas;

        private void Start()
        {
            //ApplicationMode.ChangeWorldCanvasCamera += ChangeEventCamera;

            _canvas = GetComponent<Canvas>();
        }

        //private void ChangeEventCamera() => _canvas.worldCamera = ApplicationMode.IsARMode ? _zapparCamera : _regularCamera;

        private void OnDestroy()
        {
            //ApplicationMode.ChangeWorldCanvasCamera -= ChangeEventCamera;
        }
    }
}
