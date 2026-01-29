using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ARLaboratory.Timeline
{
    [RequireComponent(typeof(PlayableDirector))]
    public class AnimationTimelineController : MonoBehaviour
    {
        [SerializeField] private TimelineController _timelineController;
        [SerializeField] private int _fastForwardSpeed = 3;

        private PlayableDirector _playableDirector;
        private Playable _playable;
    
        public event Action AnimationStart;
        public event Action AnimationEnd;
        public event Action PreviousAnimation;

        private void Start()
        {
            _playableDirector = GetComponent<PlayableDirector>();
            TimelineAsset timeline = (TimelineAsset)_playableDirector.playableAsset;
            if (timeline == null) return;
            CreatePlayable();
        }

        private void CreatePlayable()
        {
            _playableDirector.Play();
            _playable = _playableDirector.playableGraph.GetRootPlayable(0);
        }

        public void FastForward() => _playable.SetSpeed(_fastForwardSpeed);

        public void StopTimeline()
        {
            _playable.SetSpeed(0);
            _playableDirector.time = _timelineController.CurrentTimelinePosition;
            AnimationEnd?.Invoke();
        }

        public void PreviousAnimationClip()
        {
            _timelineController.CurrentMarkerIndex -= 1;
        
            if (_timelineController.CurrentMarkerIndex < 0) 
                _timelineController.CurrentMarkerIndex = _timelineController.TimelineMarkersCount - 1; 
        
            _playableDirector.time = _timelineController.CurrentTimelinePosition;
        
            PauseTimeline();
        }

        public void NextAnimationClip()
        {
            PlayTimeline();
            _timelineController.CurrentMarkerIndex += 1;
            AnimationStart?.Invoke();
        
            if (_timelineController.CurrentMarkerIndex >= _timelineController.TimelineMarkersCount)
                ResetTimeline();
        }
    
        private void PlayTimeline() => _playable.SetSpeed(1);
    
        private void PauseTimeline() 
        { 
            _playable.SetSpeed(0);
            PreviousAnimation?.Invoke();
        }

        private void ResetTimeline()
        {
            _timelineController.CurrentMarkerIndex = 0;
            _playableDirector.time = 0;
            PauseTimeline();
        }
    }
}
