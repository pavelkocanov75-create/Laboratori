using System;
using System.Collections;
using ARLaboratory.Scriptable_Object;
using UnityEngine;
using UnityEngine.Networking;

namespace ARLaboratory.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class WebAudioDownloader : AbstractLoadable, IDownloadable
    {
        [SerializeField] private ExperimentManager _experimentManager;
        [SerializeField] private int _loadingTimeout = 10;

        public event Action DownloadFailed;

        private AudioSource _audioSource;
        private bool _isAudioDownloaded = false;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            string audioUrl = _experimentManager.CurrentExperiment.WebAudio.AudioUrl;
            StartCoroutine(DownloadAudioClip(audioUrl));
        }

        public override bool IsDone() => _isAudioDownloaded;

        private IEnumerator DownloadAudioClip(string audioUrl)
        {
            using UnityWebRequest loader = UnityWebRequestMultimedia.GetAudioClip(audioUrl, AudioType.MPEG);
            loader.timeout = _loadingTimeout;
            yield return loader.SendWebRequest();
            if (loader.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("A connection error occured or the audio hasn't been found.");
                _isAudioDownloaded = true;
                DownloadFailed?.Invoke();
                yield break;
            }
        
            _audioSource.clip = DownloadHandlerAudioClip.GetContent(loader);
            _isAudioDownloaded = true;
        }
    }
}