using ARLaboratory.Core;
using UnityEngine.Playables;

namespace ARLaboratory.Timeline
{
    public class OutlineTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Outline outline = playerData as Outline;
            bool isOutline = false;
        
            int inputCount = playable.GetInputCount();
            if (IsClipsPort(inputCount)) return;
        
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0f)
                    isOutline = true;
            }
            outline.enabled = isOutline;
        }

        /// <summary>
        /// InputCount is the number open ports on the underlying playable.
        /// Clip playables have zero, because they don't need to mix anything.
        /// Track mixer has N (number of clips) because it has N clips connected.
        /// </summary>
        /// <param name="count">Number of playables on track</param>
        /// <returns>Returns true if number of ports is zero (belongs to clip, not track).</returns>
        private static bool IsClipsPort(int count) => count == 0;
    }
}