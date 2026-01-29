using UnityEngine;
using UnityEngine.Playables;

namespace ARLaboratory.Timeline
{
    public class SubtitleClip : PlayableAsset
    {
        [TextArea(1, 20)] [SerializeField] private string _subtitleText;
        private string SubtitleText => _subtitleText;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<SubtitleBehaviour> playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

            SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();

            subtitleBehaviour.subtitleText = SubtitleText;

            return playable;
        }
    }
}
