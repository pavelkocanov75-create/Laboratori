using UnityEngine;
using UnityEngine.Playables;

namespace ARLaboratory.Timeline
{
    public class WebAudioClip : PlayableAsset
    {
        [SerializeField] private int startTime;
        [SerializeField] private int endTime;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<WebAudioBehaviour> playable = ScriptPlayable<WebAudioBehaviour>.Create(graph);

            WebAudioBehaviour audioClipBehaviour = playable.GetBehaviour();

            audioClipBehaviour.startTime = startTime;

            audioClipBehaviour.endTime = endTime;

            return playable;
        }
    }
}
