using UnityEngine;

namespace ARLaboratory.Interaction
{
    public class PinchScale : MonoBehaviour
    {
        [SerializeField] private float _scaleThreshold = .002f;

        private float _startDistance;
        private float _minScale;
        private float _maxScale;

        private bool _interacting = false;

        private void Start()
        {
            Vector3 localScale = gameObject.transform.localScale;
            _minScale = localScale.x - 10f;
            _maxScale = localScale.x + 50f;
        }

        private void Update()
        {
            if (Input.touchCount == 2) 
            {
                Touch t0 = Input.GetTouch(0);
                Touch t1 = Input.GetTouch(1);

                if (!_interacting)
                {
                    _startDistance = Vector2.Distance(t1.position, t0.position);
                    _interacting = true;
                } 
                else 
                {
                    float currentDistance = Vector2.Distance(t1.position, t0.position);
                    float scalingAmount = (currentDistance - _startDistance);

                    if (!(Mathf.Abs(scalingAmount) > Mathf.Abs(_scaleThreshold))) return;
                
                    _startDistance = currentDistance;

                    Vector3 localScale = transform.localScale;
                    Vector3 newScale = new(
                        Mathf.Clamp(localScale.x + scalingAmount, _minScale, _maxScale),
                        Mathf.Clamp(localScale.y + scalingAmount, _minScale, _maxScale),
                        Mathf.Clamp(localScale.z + scalingAmount, _minScale, _maxScale)
                    );
                    localScale = newScale;
                    transform.localScale = localScale;
                }

            }
            else
            {
                _interacting = false;
            }
        }
    }
}
