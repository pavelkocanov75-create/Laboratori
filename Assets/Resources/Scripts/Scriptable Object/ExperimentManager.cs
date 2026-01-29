using UnityEngine;

namespace ARLaboratory.Scriptable_Object
{
    [CreateAssetMenu(fileName = "New Experiment Manager", menuName = "Experiment Manager")]
    public class ExperimentManager : ScriptableObject
    {
        public ExperimentData CurrentExperiment;
    }
}
