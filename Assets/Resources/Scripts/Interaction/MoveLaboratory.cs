using UnityEngine;

namespace ARLaboratory.Interaction
{
    public class MoveLaboratory : MonoBehaviour
    {
        private float _x;
        private float _z;
        private Vector3 _direction;
        private void Update()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            _x = Input.GetAxis("Horizontal");
            _z = Input.GetAxis("Vertical");
            _direction = transform.right * _x + transform.up * _z;
            transform.Translate(_direction * (2 * Time.deltaTime));
        }
    }
}
