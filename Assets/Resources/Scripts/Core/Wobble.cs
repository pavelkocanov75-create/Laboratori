using UnityEngine;

namespace ARLaboratory.Core
{
    public class Wobble : MonoBehaviour
    {
        [SerializeField] private float maxWobble = 0.03f;
        [SerializeField] private float wobbleSpeed = 1f;
        [SerializeField] private float recovery = 1f;

        private Renderer m_renderer;

        private Vector3 lastPosition;
        private Vector3 lastRotation;
        private Vector3 velocity; 
        private Vector3 angularVelocity;

        private float wobbleAmountX;
        private float wobbleAmountZ;
        private float wobbleAmountToAddX;
        private float wobbleAmountToAddZ;
        private float pulse;
        private float time = 0.5f;
    
        void Start()
        {
            m_renderer = GetComponent<Renderer>();
        }
        private void Update()
        {
            time += Time.deltaTime;

            RecoverLiquidFromWobbling();

            SinDecreaseWobbling();

            ApplyWobbleToShader();

            CalculateVelocity();

            ApplyVelocity();

            PreserveLastPosition();
        }

        private void CalculateVelocity()
        {
            velocity = (lastPosition - transform.position) / Time.deltaTime;
            angularVelocity = transform.rotation.eulerAngles - lastRotation;
        }

        private void PreserveLastPosition()
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation.eulerAngles;
        }

        private void ApplyVelocity()
        {
            wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
            wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);
        }

        private void ApplyWobbleToShader()
        {
            m_renderer.material.SetFloat("_WobbleX", wobbleAmountX);
            m_renderer.material.SetFloat("_WobbleZ", wobbleAmountZ);
        }

        private void SinDecreaseWobbling()
        {
            pulse = 2 * Mathf.PI * wobbleSpeed;
            wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
            wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);
        }

        private void RecoverLiquidFromWobbling()
        {
            wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (recovery));
            wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (recovery));
        }
    }
}