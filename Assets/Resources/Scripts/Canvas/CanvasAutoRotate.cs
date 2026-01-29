using ARLaboratory.Core;
using UnityEngine;

namespace ARLaboratory.CanvasControl
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasAutoRotate : MonoBehaviour
    {
        [Tooltip("Enables sprite's rotation by all axis")]
        [SerializeField] private bool _freeRotation = false;
        
        private Canvas _canvas;
        private Camera _currentCamera;
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            GetCurrentCamera();

            // ApplicationMode.UpdateCurrentCamera += GetCurrentCamera;
        }

        private void LateUpdate()
        {
            if (_freeRotation) { transform.forward = _currentCamera.transform.forward; }
        
            FollowCameraRotation();
        }

        private void GetCurrentCamera()
        {
            _currentCamera = _canvas.worldCamera;
        }

        private void FollowCameraRotation()
        {
            Vector3 lookPosition = transform.position - _currentCamera.transform.position;
            lookPosition.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPosition);
        }

        private void OnDestroy()
        {
            //ApplicationMode.UpdateCurrentCamera -= GetCurrentCamera;
        }
    }
}
