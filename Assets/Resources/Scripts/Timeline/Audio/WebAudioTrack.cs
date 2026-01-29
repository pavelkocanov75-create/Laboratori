using UnityEngine.Timeline;

namespace ARLaboratory.Timeline
{
    [TrackBindingType(typeof(TimelineAudioController))]
    [TrackClipType(typeof(WebAudioClip))]
    public class WebAudioTrack : TrackAsset { }
}

