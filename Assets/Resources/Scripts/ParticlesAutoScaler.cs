using UnityEngine;

namespace ARLaboratory
{
    public class ParticlesAutoScaler : MonoBehaviour
    {
        private ParticleSystem _particles;
        [SerializeField] private float _sliderValue = 1.0F;
        [SerializeField] private ParticleSystemScalingMode _scaleMode;

        private void Start()
        {
            _particles = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            _particles.transform.localScale = new Vector3(_sliderValue, _sliderValue, _sliderValue);
            var mainModule = _particles.main;
            mainModule.scalingMode = _scaleMode;
        }
    }
}
