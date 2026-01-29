using ARLaboratory.Scriptable_Object;
using TMPro;
using UnityEngine;

namespace ARLaboratory.Core
{
    public class ExperimentSelectionMenu : MonoBehaviour
    {
        [SerializeField] private Animator _screenTransition;
        [SerializeField] private GameObject _selectionButton;
        [SerializeField] private GameObject _scrollContent;
    
        [Space(10)]
        [SerializeField] private ExperimentData[] _experiments;
    
        private void Start()
        {
            foreach (ExperimentData experiment in _experiments)
            { 
                CreateSelectionMenuButton(experiment);
            }
        }
    
        private void CreateSelectionMenuButton(ExperimentData experiment)
        {
            GameObject button = Instantiate(_selectionButton, _scrollContent.transform, false);
            TextMeshProUGUI label = button.transform.Find("Content/Label").GetComponent<TextMeshProUGUI>();
            button.GetComponent<SelectExperiment>().SelectedExperiment = experiment;
            button.GetComponent<LevelLoader>().CrossfadeAnimator = _screenTransition;
            label.text = experiment.Header;
        }
    }
}
