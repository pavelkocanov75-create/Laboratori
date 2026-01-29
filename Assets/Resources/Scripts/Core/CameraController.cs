using ARLaboratory;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _target;

    [Header("Camera Rotation")]
    [Range(0f, 1f)] 
    [SerializeField] private float _cameraSpeed = 1f;
    [Range(0f, 10f)] 
    [SerializeField] private float scrollSpeed = 8f;

    [Header("Camera Zoom")]
    [SerializeField] private float _zoomMinimum = 3.0f;
    [SerializeField] private float _zoomMaximum = 8.0f;

    [Header("Camera Move")]
    [Range (0f, 1f)]
    [SerializeField] private float _moveSpeed = 0.05f;

    private float _currentDistance;

    private float _xAngle = 0f;
    private float _yAngle = 0f;

    private Vector3 _dragOrigin;

    private Vector3 _originalCameraPosition;
    private Quaternion _originalCameraRotation;

    private Vector3 _originalTargetPosition;

    private void Start()
    {
        _originalCameraPosition = transform.localPosition;
        _originalCameraRotation = transform.localRotation;

        _originalTargetPosition = _target.localPosition;

        Vector3 angles = transform.eulerAngles;

        _xAngle = angles.x;
        _yAngle = angles.y;

        _currentDistance = Vector3.Distance(transform.position, _target.position);
    }

    private void LateUpdate()
    {
        
        // PC Contol
        _currentDistance = Vector3.Distance(transform.position, _target.position);

        if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && Input.touchCount != 2)
        {
            float verticalMouseInput = Input.GetAxis("Mouse Y");
            float horizontalMouseInput = Input.GetAxis("Mouse X");
            RotateCamera(verticalMouseInput, horizontalMouseInput);
        }

        RotateCameraOnKeys();

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float zoomAmount = Input.GetAxis("Mouse ScrollWheel");
            ZoomCamera(zoomAmount);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            MoveCamera();
        }

        // Mobile Control
        if (Input.touchCount == 2)
        {
            Touch touch_0 = Input.GetTouch(0);
            Touch touch_1 = Input.GetTouch(1);

            if (touch_0.phase == TouchPhase.Moved && touch_1.phase == TouchPhase.Moved) 
            {
                Vector2 touchDelta = (touch_0.deltaPosition + touch_1.deltaPosition) / 2;
                MoveCameraMobile(touchDelta);
            }

            float currentDistance = Vector2.Distance(touch_0.position, touch_1.position);
            float previousDistance = Vector2.Distance(touch_0.position - touch_0.deltaPosition, touch_1.position - touch_1.deltaPosition);
            float pinchDelta = (currentDistance - previousDistance) * Time.deltaTime;

            ZoomCamera(pinchDelta);
        }
    }

    public void ResetCameraTransformation()
    {
        ResetPosition();

        ResetDistance();

        ResetRotation();

        transform.LookAt(_target);
    }

    private void ResetPosition()
    {
        transform.localPosition = _originalCameraPosition;
        transform.localRotation = _originalCameraRotation;

        _target.localPosition = _originalTargetPosition;
    }

    private void ResetDistance()
    {
        Vector3 direction = transform.position - _target.position;
        _currentDistance = direction.magnitude;
    }

    private void ResetRotation()
    {
        Vector3 angles = transform.eulerAngles;
        _xAngle = angles.y;
        _yAngle = angles.x;
    }

    private void RotateCamera(float verticalMouseInput, float horizontalMouseInput)
    {
        float _yMinLimit = 0f;
        float _yMaxLimit = 90f;

        float _xMinLimit = -90f;
        float _xMaxLimit = 90f;

        float newCameraSpeed = CalculateCameraRotationSpeed(_cameraSpeed);

        _xAngle += horizontalMouseInput * newCameraSpeed * Time.deltaTime;
        _yAngle += verticalMouseInput * newCameraSpeed * Time.deltaTime;

        _yAngle = ClampAngle(_yAngle, _yMinLimit, _yMaxLimit);
        _xAngle = ClampAngle(_xAngle, _xMinLimit, _xMaxLimit);

        Quaternion rotation = Quaternion.Euler(_yAngle, _xAngle, 0f);

        Vector3 position = rotation * new Vector3(0f, 0f, -_currentDistance) + _target.position;

        transform.SetPositionAndRotation(position, rotation);
    }

    private float CalculateCameraRotationSpeed(float camSpeed)
    {
        float minCameraSpeed = 200f;
        float maxCameraSpeed = 700f;
        return Mathf.Lerp(minCameraSpeed, maxCameraSpeed, camSpeed);
    }

    private void RotateCameraOnKeys()
    {
        float keyRotationSpeed = 0.2f;
        float keyHorizontal = 0f;
        float keyVertical = 0;

        if (Input.GetKey(KeyCode.W))
        {
            keyHorizontal += keyRotationSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            keyHorizontal -= keyRotationSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            keyVertical += keyRotationSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            keyVertical -= keyRotationSpeed;
        }

        if (keyHorizontal != 0 || keyVertical != 0)
        {
            RotateCamera(keyHorizontal, keyVertical);
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }


    private void MoveCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 clampX = new(-1f, 1f);
            Vector2 clampY = new(0f, 1f);

            Vector3 diff = Input.mousePosition - _dragOrigin;

            Vector3 move = new Vector3(-diff.x, -diff.y, 0) * _moveSpeed * Time.deltaTime;

            Vector3 newPosition = _target.localPosition + _target.TransformVector(move);

            newPosition.x = Mathf.Clamp(newPosition.x, clampX.x, clampX.y);
            newPosition.y = Mathf.Clamp(newPosition.y, clampY.x, clampY.y);

            _target.localPosition = newPosition;

            _dragOrigin = Input.mousePosition;

            // Update the camera's position and rotation to follow the target
            Quaternion rotation = Quaternion.Euler(_yAngle, _xAngle, 0f);
            Vector3 cameraPosition = rotation * new Vector3(0f, 0f, -_currentDistance) + _target.position;
            transform.SetPositionAndRotation(cameraPosition, rotation);
        }
    }

    private void MoveCameraMobile(Vector2 touchDelta)
    {
        Vector3 move = new Vector3(-touchDelta.x, -touchDelta.y, 0) * _moveSpeed * Time.deltaTime;
        _target.position += transform.TransformDirection(move);

        Quaternion rotation = Quaternion.Euler(_yAngle, _xAngle, 0f);
        Vector3 cameraPosition = rotation * new Vector3(0f, 0f, -_currentDistance) + _target.position;
        transform.position = cameraPosition;
    }

    private void ZoomCamera(float zoomAmount)
    {
        float zoomTranslate = zoomAmount * scrollSpeed; 

        float newDistance = _currentDistance - zoomTranslate;

        _currentDistance = Mathf.Clamp(newDistance, _zoomMinimum, _zoomMaximum);

        transform.position = _target.position - transform.forward * _currentDistance;

        transform.LookAt(_target.position);
    }
}
