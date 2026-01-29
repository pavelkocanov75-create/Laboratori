using UnityEngine;
using UnityEngine.EventSystems;

namespace ARLaboratory.Interaction
{
    public class DragRotate : MonoBehaviour, IDragHandler
    {
        [SerializeField] private GameObject _targetObject;
    
        [SerializeField] private int _rotationSpeed = 15;

        private int _invert;
        private float _xRotation;
        private float _yRotation;
        
        public void OnDrag(PointerEventData eventData)
        {
            _invert = IsNotCounterDirectional() ? -1 : 1;
            _xRotation = _invert * eventData.delta.y * Time.deltaTime * _rotationSpeed;
            _yRotation = -eventData.delta.x * Time.deltaTime * _rotationSpeed;
            _targetObject.transform.eulerAngles += new Vector3(_xRotation, _yRotation, 0);
        }

        private bool IsNotCounterDirectional() => Vector3.Dot(_targetObject.transform.up, Vector3.up) >= 0;
    }
}
