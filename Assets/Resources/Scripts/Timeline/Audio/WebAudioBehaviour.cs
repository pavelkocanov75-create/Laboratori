using UnityEngine.Playables;

namespace ARLaboratory.Timeline
{
    public class WebAudioBehaviour : PlayableBehaviour
    {
        public int startTime;
        public int endTime;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TimelineAudioController audioController = playerData as TimelineAudioController;
        
            audioController.StartTime = startTime;
    
            audioController.EndTime = endTime;
        }
    }
}
