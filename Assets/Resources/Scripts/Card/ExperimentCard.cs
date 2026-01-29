using ARLaboratory.Scriptable_Object;
using TMPro;
using UnityEngine;

namespace ARLaboratory.Card
{
    public class ExperimentCard : MonoBehaviour
    {
        [SerializeField] private ExperimentManager _experimentManager;

        [Header("Text Fields")]
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _about;

        private Animator _experimentCardAnimator;

        private void Awake()
        {
            _experimentCardAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            _header.text = _experimentManager.CurrentExperiment.Header;
            _about.text = _experimentManager.CurrentExperiment.Description;
        }

        public void OpenAnimation()
        {
            _experimentCardAnimator.SetTrigger("Start");
        }

        public void CloseAnimation()
        {
            _experimentCardAnimator.SetTrigger("Close");
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}
