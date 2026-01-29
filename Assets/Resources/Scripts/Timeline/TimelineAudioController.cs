using System.Collections;
using ARLaboratory.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ARLaboratory.Timeline
{
    [RequireComponent(typeof(WebAudioDownloader))]
    public class TimelineAudioController : MonoBehaviour
    {
        [SerializeField] private Image _audioFill;
    
        [Header("Timeline Audio Buttons")]
        [SerializeField] private GameObject _playButton;
        [SerializeField] private GameObject _stopButton;
        public int StartTime { get; set; } = 0;
        public int EndTime { get; set; } = 0;

        private AudioSource _audioSource;
        private IEnumerator _coroutine;
        private float _ratio;

        private void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            if (!_audioSource.isPlaying) return;
            FillAudioProgress();
        }

        private void FillAudioProgress()
        {
            _audioFill.fillAmount += Mathf.Lerp(0, 1, _ratio * Time.deltaTime);
        }

        public void PlayAudio()
        {
            _coroutine = SetScheduledAudio();

            StartCoroutine(_coroutine);
        }

        public void StopAudio()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _audioSource.Stop();
            _audioFill.fillAmount = 0;
            EnablePlayButton();
        }

        private IEnumerator SetScheduledAudio()
        {
            double currentTime = AudioSettings.dspTime;
            double duration = EndTime - StartTime;

            _ratio = 1f / (float)duration;
            _audioSource.time = StartTime;

            DisablePlayButton();

            StartAudioSource(currentTime, duration);

            yield return new WaitForSeconds((float)duration);
        
            _audioFill.fillAmount = 0;

            EnablePlayButton();
        }

        private void StartAudioSource(double currentTime, double duration)
        {
            _audioSource.Play();
            _audioSource.SetScheduledEndTime(currentTime + duration);
        }

        private void EnablePlayButton()
        {
            _playButton.SetActive(true);
            _stopButton.SetActive(false);
        }

        private void DisablePlayButton()
        {
            _playButton.SetActive(false);
            _stopButton.SetActive(true);
        }
    }
}
