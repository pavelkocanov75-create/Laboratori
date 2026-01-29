using System;
using ARLaboratory.Scriptable_Object;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using ARLaboratory.Core;
#if UNITY_WEBGL && !UNITY_EDITOR
using AOT;
using System.Runtime.InteropServices;
#endif

namespace ARLaboratory.Card
{
    public class WebVideoPlayer : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport( "__Internal" )]
    private static extern bool CheckForWebGLIOS();
#else
        private static bool CheckForWebGLIOS()
        {
            return false;
        }
#endif

        [SerializeField] private ExperimentManager _experimentManager;

        [SerializeField] private Slider _videoSlider;
        [SerializeField] private RenderTexture _renderTexture;

        [Header("Video Player Text")]
        [SerializeField] private TextMeshProUGUI _currentMimutes;
        [SerializeField] private TextMeshProUGUI _currentSeconds;
        [SerializeField] private TextMeshProUGUI _totalMinutes;
        [SerializeField] private TextMeshProUGUI _totalSeconds;

        [Header("Video Player Buttons")]
        [SerializeField] private GameObject _playButton;
        [SerializeField] private GameObject _pauseButton;

        private VideoPlayer _videoPlayer;
        private GameObject _playerGameObject;
        private AudioSource _audioSource;
    
        private float _ratio;
        private bool _isPlayerGameObjectNull;

        private double VideoRatio => (double)_videoPlayer.frame / (double)_videoPlayer.frameCount;

        private void Start()
        {
            _isPlayerGameObjectNull = _playerGameObject == null;
        }


        private void Update()
        {
            if (_isPlayerGameObjectNull) return;
        
            if (_videoPlayer.isPrepared)
            {
                CalculateVideoTime();
                _videoSlider.value = (float)VideoRatio;
            }

            if (!_videoPlayer.isPlaying) return;
        
            SetCurrentTime();
        
            _videoSlider.value += _ratio * Time.deltaTime;
        }

        public void CreateVideoPlayer()
        {
            _playerGameObject = new GameObject("Video Player");

            _videoPlayer = _playerGameObject.AddComponent<VideoPlayer>();

            _videoPlayer.gameObject.transform.SetParent(gameObject.transform, false);

            _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            _videoPlayer.targetTexture = _renderTexture;
            _videoPlayer.aspectRatio = VideoAspectRatio.FitInside;

#if !UNITY_EDITOR && UNITY_WEBGL
        if (CheckForWebGLIOS() == true)
        {
            _videoPlayer.waitForFirstFrame = false;
            _videoPlayer.playOnAwake = false;
        }
            
        // WEBGL Requires you to use Direct audio type
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
#else
            _audioSource = _videoPlayer.gameObject.AddComponent<AudioSource>();

            _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

            _videoPlayer.SetTargetAudioSource(0, _audioSource);
#endif

            SetVolume(1);

            _videoPlayer.source = VideoSource.Url;

            _videoPlayer.isLooping = true;

            string videoUrl = _experimentManager.CurrentExperiment.VideoUrl;
        
            SetVideoUrl(videoUrl);
        }

        private void CalculateVideoTime()
        {
            double time = _videoPlayer.frameCount / _videoPlayer.frameRate;

            TimeSpan videoClipLength = TimeSpan.FromSeconds(time);

            _totalMinutes.text = (videoClipLength.Minutes).ToString("00");
            _totalSeconds.text = (videoClipLength.Seconds).ToString("00");
        }

        public void SetVideoUrl(string url)
        {
            _videoPlayer.url = url;

#if !UNITY_EDITOR && UNITY_WEBGL
        if (CheckForWebGLIOS() == true)
        {
            IOSWebPlaybackHelper.Instance.AddToVideoList(gameObject.name);
            IOSWebPlaybackHelper.Instance.ShowHtmlButton();
        }
#endif
        }

        private void ResetParameters()
        {
            _videoSlider.value = 0f;
            _videoPlayer.time = 0f;
            _playButton.SetActive(true);
            _pauseButton.SetActive(false);
        }

        private void SetCurrentTime()
        {
            _currentMimutes.text = Mathf.Floor((int)_videoPlayer.time / 60).ToString("00");
            _currentSeconds.text = Mathf.Floor((int)_videoPlayer.time % 60).ToString("00");
        }

        public void Play() => _videoPlayer.Play();

        public void Pause() => _videoPlayer.Pause();

        public void Stop() => ResetParameters();

        public void CloseVideoPlayer() => Destroy(_playerGameObject);

        public void ToggleAudio() {
            if (IsMuted() == true) 
            {
                UnMute();
            } 
            else 
            {
                Mute();
            }
        }

        public bool  IsMuted(){
#if !UNITY_EDITOR && UNITY_WEBGL
        return _videoPlayer.GetDirectAudioMute(0);
#else
            return _videoPlayer.GetTargetAudioSource(0).mute;
#endif
        }

        public void Mute() {
#if !UNITY_EDITOR && UNITY_WEBGL
        _videoPlayer.SetDirectAudioMute(0, true);
#else
            _videoPlayer.GetTargetAudioSource(0).mute = true;
#endif

        }

        public void UnMute() {

#if !UNITY_EDITOR && UNITY_WEBGL
        _videoPlayer.SetDirectAudioMute(0, false);
#else
            _videoPlayer.GetTargetAudioSource(0).mute = false;
#endif

        }

        public void SetVolume(float vol) {
            if (vol > 1) {
                vol = 1;
            } else if (vol < 0) {
                vol = 0;
            }

#if !UNITY_EDITOR && UNITY_WEBGL
        _videoPlayer.SetDirectAudioVolume(0, vol);
#else
            _audioSource.volume = vol;
#endif

        } 

    }
} 