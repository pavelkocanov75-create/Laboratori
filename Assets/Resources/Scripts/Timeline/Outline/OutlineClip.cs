using UnityEngine;
using UnityEngine.Playables;

namespace ARLaboratory.Timeline
{
    public class OutlineClip : PlayableAsset
    {
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<OutlineTrackMixer> playable = ScriptPlayable<OutlineTrackMixer>.Create(graph);
        
            return playable;
        }
    }
}
