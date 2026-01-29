using ARLaboratory.Core;
using UnityEngine;

namespace ARLaboratory.CanvasControl
{
    public class CameraDistanceScaler : MonoBehaviour
    {
        [SerializeField] private float _maxScale = 0.1f;

        [SerializeField] private float _minDistance = 1.5f;
        [SerializeField] private float _maxDistance = 3f;
        
        private float _currentScale;
        private Camera _camera;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            //ApplicationMode.UpdateCurrentCamera += GetEventCamera;
            GetEventCamera();
            _currentScale = transform.localScale.x;
        }

        private void Update()
        {
            ScaleByDistance();
        }

        private void ScaleByDistance()
        {
            float distance = Vector3.Distance(transform.position, _camera.transform.position);
            float limitedDistance = Mathf.InverseLerp(_minDistance, _maxDistance, distance);
            float scale = Mathf.Lerp(_currentScale, _maxScale, limitedDistance);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        private void GetEventCamera()
        {
            _camera = GetComponent<UnityEngine.Canvas>().worldCamera;
        }

        private void OnDestroy()
        {
            //ApplicationMode.UpdateCurrentCamera -= GetEventCamera;
        }
    }
}
