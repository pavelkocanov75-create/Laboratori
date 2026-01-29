using UnityEngine;
using UnityEngine.EventSystems;

namespace ARLaboratory.Interaction
{
    public class InteractionsController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _moveMultiplayer = 0.0005f;
        [SerializeField] private Vector2 _minTransform;
        [SerializeField] private Vector2 _maxTransform;
        [Header("Scaling")]
        [SerializeField] private float _scaleMultiplier = .01f;
        [SerializeField] private float _minScale = .1f;
        [SerializeField] private float _maxScale = 6.5f;
        [Header("Rotation")]
        [SerializeField] private float _angleThreshold = .2f;
        [SerializeField] private float _scaleThreshold = .002f;
        
        private Touch _firstTouch;
        private Touch _secondTouch;
        
        private Vector2 _startPosition;
        private float _startDistance;
        private bool _interacting = false;

        private static bool IsPinching => Input.touchCount == 2;
        private static bool IsTouching => Input.touchCount == 1;
        private bool IsTouchMoving => _firstTouch.phase == TouchPhase.Moved;

        private void Update() 
        {
            if (IsTouching)
            {
                if (IsTouchOverObject())
                    return;
                
                _firstTouch = Input.GetTouch(0);
                if (IsTouchMoving)
                    MoveObject();
            }
            else if (IsPinching)
            {
                if (IsTouchOverObject())
                    return;

                _firstTouch = Input.GetTouch(0);
                _secondTouch = Input.GetTouch(1);
                
                if(!_interacting)
                {
                    StartInteraction();
                }
                else
                {
                    Vector2 currentPosition = _secondTouch.position - _firstTouch.position;
                    float angleOffset = Vector2.Angle(_startPosition, currentPosition);
                    var cross = Vector3.Cross(_startPosition, currentPosition);
                    bool isRotating = angleOffset > _angleThreshold;
                    
                    if (isRotating)
                    {
                        RotateObject(angleOffset, currentPosition, cross);
                    }

                    float currentDistance = Vector2.Distance(_secondTouch.position, _firstTouch.position);
                    float scalingAmount = (currentDistance - _startDistance) * _scaleMultiplier;
                    bool isScaling = Mathf.Abs(scalingAmount) > Mathf.Abs(_scaleThreshold);
                    
                    if (isScaling)
                    {
                        _startDistance = currentDistance;
                        ScaleObject(scalingAmount);
                    }

                }
            }
            else
            {
                _interacting = false;
            }
        }

        private bool IsTouchOverObject() => EventSystem.current.IsPointerOverGameObject();
        
        private void StartInteraction()
        {
            _startPosition = _secondTouch.position - _firstTouch.position;
            _startDistance = Vector2.Distance(_secondTouch.position, _firstTouch.position);
            _interacting = true;
        }
        
        private void MoveObject()
        {
            var localPosition = transform.localPosition;
            var localPositionX = localPosition.x - (_firstTouch.deltaPosition.x * _moveMultiplayer);
            var localPositionZ = localPosition.z - (_firstTouch.deltaPosition.y * _moveMultiplayer);
            var newPosition = new Vector3(
                Mathf.Clamp(localPositionX, _minTransform.x, _maxTransform.x),
                localPosition.y,
                Mathf.Clamp(localPositionZ, _minTransform.y, _maxTransform.y)
            );
            transform.localPosition = newPosition;
        }

        private void ScaleObject(float scalingAmount)
        {
            var localScale = transform.localScale;
            var newScale = new Vector3(
                Mathf.Clamp(localScale.x + scalingAmount, _minScale, _maxScale),
                Mathf.Clamp(localScale.y + scalingAmount, _minScale, _maxScale),
                Mathf.Clamp(localScale.z + scalingAmount, _minScale, _maxScale)
            );
            transform.localScale = newScale;
        }

        private void RotateObject(float angleOffset, Vector2 currentPosition, Vector3 cross)
        {
            _startPosition = currentPosition;
            if (cross.z > 0)
                transform.RotateAround(transform.position, transform.up, -angleOffset);
            else if (cross.z < 0)
                transform.RotateAround(transform.position, transform.up, angleOffset);
        }
    }
}
