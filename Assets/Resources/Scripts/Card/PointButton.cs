using System.Collections;
using UnityEngine;

namespace ARLaboratory.Card
{
    public class PointButton : MonoBehaviour
    {
        [SerializeField] private ExperimentCard _experimentCard;
        [Header("Buttons")] 
        [SerializeField] private GameObject _openButton;
        [SerializeField] private GameObject _closeButton;

        public static ExperimentCard Card;
    
        private readonly WaitForSeconds _waitForAnimator = new(.75f);

        private void OnDisable()
        {
            _experimentCard.Deactivate();
            EnableOpenButton();
        }

        public void OpenCard()
        {
            ActivateExperimentCard();
            CloseActiveDataPoint();
            EnableCloseButton();
        }

        public void CloseCard()
        {
            DeactivateExperimentCard(_experimentCard);
            EnableOpenButton();
        }
        
        private void EnableCloseButton()
        {
            _openButton.SetActive(false);
            _closeButton.SetActive(true);
        }

        private void EnableOpenButton()
        {
            _openButton.SetActive(true);
            _closeButton.SetActive(false);
        }

        private void DeactivateExperimentCard(ExperimentCard experimentCard)
        {
            StartCoroutine(CO_DeactivateExperimentCard(experimentCard));
        }

        private void CloseActiveDataPoint()
        {
            if (Card == null)
            {
                Card = _experimentCard;
            }
            else if (Card != _experimentCard)
            {
                DeactivateExperimentCard(Card);
                Card = _experimentCard;
            }
        }
    
        private void ActivateExperimentCard()
        {
            _experimentCard.Activate();
            _experimentCard.OpenAnimation();
        }

        private IEnumerator CO_DeactivateExperimentCard(ExperimentCard experimentCard)
        {
            experimentCard.CloseAnimation();
            yield return _waitForAnimator;
            experimentCard.Deactivate();
        }
    }
}
