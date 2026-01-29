using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARLaboratory.Card
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _audioPanel;
        [SerializeField] private Slider _audioSlider;
    
        [Header("Audio Time Text")]
        [SerializeField] private TextMeshProUGUI _currentMinutes;
        [SerializeField] private TextMeshProUGUI _currentSeconds;
        [SerializeField] private TextMeshProUGUI _totalMinutes;
        [SerializeField] private TextMeshProUGUI _totalSeconds;

        [Header("Audio Buttons")]
        [SerializeField] private GameObject _playButton;
        [SerializeField] private GameObject _pauseButton;
    
        [Header("Card Audio Icons")]
        [SerializeField] private GameObject _playIcon;
        [SerializeField] private GameObject _pauseIcon;

        private AudioSource AudioSource { get; set; }
    
        private float _totalTime;
        private float _ratio;
        private float _remainingTime;
        private bool _isTotalTimeSet = false;

        private void Start() 
        {
            AudioSource = GetComponent<AudioSource>();
        }
        private void FixedUpdate()
        {
            if (!AudioSource.isPlaying) return;

            if (!_isTotalTimeSet)
                ShowTotalTime();
        
            ShowCurrentAudioProgress();
        
            _remainingTime = (_totalTime - GetCurrentAudioTime()) / AudioSource.pitch;
            if (IsAudioEnded(_remainingTime))
                StopAudio();
        }

        private void OnDisable()
        {
            StopAudio();
        }

        private void ShowTotalTime()
        {
            _totalTime = GetTotalAudioTime();
            SetAudioTime(_totalTime, _totalMinutes, _totalSeconds);
            _ratio = 1f / _totalTime;
            _isTotalTimeSet = true;
        }

        private void ShowCurrentAudioProgress()
        {
            SetAudioTime(GetCurrentAudioTime(), _currentMinutes, _currentSeconds);
            _audioSlider.value += _ratio * Time.deltaTime;
        }
    
        private float GetTotalAudioTime() => AudioSource.clip.length;
    
        private float GetCurrentAudioTime() => AudioSource.time;

        private void SetAudioTime(float audioTimeLength, TMP_Text minutesText, TMP_Text secondsText)
        {
            minutesText.text = CalculateMinutes(audioTimeLength).ToString("00");
            secondsText.text = CalculateSeconds(audioTimeLength).ToString("00");
        }

        private float CalculateMinutes(float time) => Mathf.Floor((int)time / 60);

        private float CalculateSeconds(float time) => Mathf.Floor((int)time % 60);

        private bool IsAudioEnded(float remainingTime) => Mathf.Floor(remainingTime) == 0f;
    
        public void PauseAudio() 
        {
            AudioSource.Pause();

            _playIcon.SetActive(false);
            _pauseIcon.SetActive(true);
        }

        public void PlayAudio() 
        {
            AudioSource.Play();

            _audioSlider.gameObject.SetActive(true);
            _audioPanel.gameObject.SetActive(true);

            _playIcon.SetActive(true);
            _pauseIcon.SetActive(false);
        }

        public void StopAudio() 
        {
            AudioSource.Stop();
            ResetParameters();
        }

        private void ResetParameters() 
        {
            AudioSource.time = 0f;

            _audioSlider.value = 0f;

            _audioSlider.gameObject.SetActive(false);
            _audioPanel.gameObject.SetActive(false);

            _playButton.SetActive(true);
            _pauseButton.SetActive(false);

            _playIcon.SetActive(false);
            _pauseIcon.SetActive(true);
        }
    }
}