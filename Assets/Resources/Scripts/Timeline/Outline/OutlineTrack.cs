using ARLaboratory.Core;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ARLaboratory.Timeline
{
    [TrackBindingType(typeof(Outline))]
    [TrackClipType(typeof(OutlineClip))]
    public class OutlineTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<OutlineTrackMixer>.Create(graph, inputCount);
        }
    }
}