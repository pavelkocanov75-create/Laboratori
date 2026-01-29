using ARLaboratory.Scriptable_Object;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ARLaboratory
{
    public class LaboratorySceneManager : MonoBehaviour
    {
        [SerializeField] private ExperimentManager _experimentManager;
        [SerializeField] private PlayableDirector _playableDirector;

        public UnityEvent OnTimelineNotExist;

        private ExperimentData _experiment;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _experiment = _experimentManager.CurrentExperiment;
            SetCameraPosition();
            AssignExperimentTimelineToDirector();
        }

        private void SetCameraPosition()
        {
            if (_experiment.OverwriteCameraPosition)
            {
                _camera.transform.localPosition = _experiment.StartCameraPosition;
            }
        }

        private void AssignExperimentTimelineToDirector()
        {
            TimelineAsset experimentTimeline = _experimentManager.CurrentExperiment.TimelineAsset;
            if (experimentTimeline == null)
            {
                OnTimelineNotExist?.Invoke();
                return;
            }
            _playableDirector.playableAsset = experimentTimeline;
        }
    }
}
