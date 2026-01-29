using TMPro;
using UnityEngine;

namespace ARLaboratory.Timeline
{
    [RequireComponent(typeof(AnimationTimelineController))]
    public class TimelineUI : MonoBehaviour
    {
        [SerializeField] private TimelineController _timelineController;
    
        [Header("Animation Status Buttons")]
        [SerializeField] private GameObject _idleButton;
        [SerializeField] private GameObject _activeButton;
    
        [Header("Text Fields")]
        [SerializeField] private TextMeshProUGUI _currentStepText;
        [SerializeField] private TextMeshProUGUI _totalStepsText;

        private AnimationTimelineController _animationTimeline;
        private int StepIndex => _timelineController.CurrentMarkerIndex == 0 ? 1 : _timelineController.CurrentMarkerIndex + 1;

        private void Awake()
        {
            _animationTimeline = GetComponent<AnimationTimelineController>();
        
            _animationTimeline.AnimationStart += StartAnimationUpdate;
        
            _animationTimeline.PreviousAnimation += PreviousAnimationUpdate;

            _animationTimeline.AnimationEnd += EndAnimationUpdate;
        }

        private void Start()
        {
            Invoke("DisplayTotalIndex", .5f);
        }
    
        private void StartAnimationUpdate()
        {
            ShowActiveButton();
        }

        private void PreviousAnimationUpdate()
        {
            DisplayCurrentIndex();
        }
    
        private void EndAnimationUpdate()
        {
            DisplayCurrentIndex();
            ShowIdleButton();
        }

        private void DisplayCurrentIndex() => _currentStepText.text = StepIndex.ToString();
    
        private void DisplayTotalIndex() => _totalStepsText.text = _timelineController.TimelineMarkersCount.ToString();

        private void ShowActiveButton() 
        {
            _idleButton.SetActive(false);
            _activeButton.SetActive(true);
        }
        private void ShowIdleButton()
        {
            _idleButton.SetActive(true);
            _activeButton.SetActive(false);
        }
    
        private void OnDestroy()
        {
            _animationTimeline.AnimationStart -= StartAnimationUpdate;
        
            _animationTimeline.PreviousAnimation -= PreviousAnimationUpdate;

            _animationTimeline.AnimationEnd -= EndAnimationUpdate;
        }
    }
}
