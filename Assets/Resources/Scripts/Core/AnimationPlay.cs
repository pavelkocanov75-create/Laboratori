using UnityEngine;

namespace ARLaboratory.Core
{
    public class AnimationPlay : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private bool _isActive = false;
        private static readonly int Start = Animator.StringToHash("Start");
        private static readonly int Close = Animator.StringToHash("Close");

        public void Toggle()
        {
            int animationTrigger = _isActive ? Close : Start;
            _animator.SetTrigger(animationTrigger);
            _isActive = !_isActive;
        }
        
        public void Play()
        {
            _animator.SetTrigger(Start);
            _isActive = true;
        }

        public void Stop()
        {
            _animator.SetTrigger(Close);
            _isActive = false;
        }
    }
}
