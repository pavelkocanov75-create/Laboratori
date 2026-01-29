using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ARLaboratory.Timeline
{
    [RequireComponent(typeof(PlayableDirector))]
    public class TimelineController : MonoBehaviour
    {
        public int CurrentMarkerIndex { get; set; } = 0;
        public int TimelineMarkersCount { get; private set; } = 0;
        public double CurrentTimelinePosition => _timelineMarkers[CurrentMarkerIndex].time + Error;

        private List<Marker> _timelineMarkers;
        private TimelineAsset _timeline;
        private PlayableDirector _playableDirector;
    
        private delegate MarkerTrack MarkersTrackDelegate();
    
        private const string StopMarker = "StopTimeline";
    
        // add to marker time to put current timeline a little bit further than stop marker so the head wouldn't hit it after play again
        private const double Error = 0.0001;
    
        private void Start()
        {
            _playableDirector = GetComponent<PlayableDirector>();
        
            _timeline = (TimelineAsset)_playableDirector.playableAsset;
        
            if (_timeline == null) return;
        
            _timelineMarkers = CollectMarkers(TimelineMarkerTrack, StopMarker);
            TimelineMarkersCount = _timelineMarkers.Count;
        }

        public List<Marker> GetTimelineMarkers() => _timelineMarkers;
    
        private List<Marker> CollectMarkers(MarkersTrackDelegate markerTrack, string markerType)
        {
            List<Marker> markers = new();
            MarkerTrack track = markerTrack();
            IMarker[] sortedMarkers = track.GetMarkers().OrderBy((marker) => marker.time).ToArray();
        
            foreach (var marker in sortedMarkers.Cast<Marker>())
            {
                if (IsValidMarker(marker, markerType))
                    markers.Add(marker);
            }
            return markers;
        }
    
        private MarkerTrack TimelineMarkerTrack() => _timeline.markerTrack;
    
        private static bool IsValidMarker(Marker marker, string markerType)
        {
            SignalEmitter signal = (SignalEmitter)marker;
            string signalName = signal.asset.name;
            return signalName == markerType;
        }
    }
}
