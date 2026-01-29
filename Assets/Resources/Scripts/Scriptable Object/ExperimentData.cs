using System;
using ARLaboratory.Core;
using UnityEngine;
using UnityEngine.Timeline;

namespace ARLaboratory.Scriptable_Object
{
    [CreateAssetMenu(fileName = "New Experiment Data", menuName = "Experiment Data")]
    public class ExperimentData : ScriptableObject
    {
        [SerializeField] private TimelineAsset _experimentTimeline;

        [SerializeField] private bool _overwriteCameraPosition;
        
        [SerializeField] private Vector3 _startCameraPosition;

        [SerializeField] private string _header;

        [TextArea(1, 20)]
        [SerializeField] private string _description;

        [SerializeField] private WebAudioData _webAudio;

        [SerializeField] private string _videoUrl;

        [SerializeField] private string[] _imageLinks;

        [SerializeField] private Equipment[] _reagents;

        [SerializeField] private Equipment[] _equipment;

        public TimelineAsset TimelineAsset => _experimentTimeline;
        public bool OverwriteCameraPosition => _overwriteCameraPosition;
        public Vector3 StartCameraPosition => _startCameraPosition;
        public string Header => _header;
        public string Description => _description;
        public WebAudioData WebAudio => _webAudio;
        public string VideoUrl => _videoUrl;
        public string[] ImageLinks => _imageLinks;
        public Equipment[] Reagents => _reagents;
        public Equipment[] Equipment => _equipment;

        private const int ListSize = 9;

        private void OnValidate()
        {
            if (_imageLinks.Length != ListSize)
            {
                Array.Resize(ref _imageLinks, ListSize);
            }
        }
    }
}
