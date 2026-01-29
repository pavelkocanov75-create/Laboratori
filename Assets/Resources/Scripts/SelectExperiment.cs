using ARLaboratory;
using ARLaboratory.Scriptable_Object;
using UnityEngine;

public class SelectExperiment : MonoBehaviour
{
    [SerializeField] private ExperimentManager _experimentManager;
    
    public ExperimentData SelectedExperiment { get; set; }

    public void StartSelectedExperiment()
    {
        _experimentManager.CurrentExperiment = SelectedExperiment;
    }
}
