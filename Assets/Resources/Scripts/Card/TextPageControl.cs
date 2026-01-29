using System.Collections;
using TMPro;
using UnityEngine;

namespace ARLaboratory.Card
{
    public class TextPageControl : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cardText;
        [SerializeField] private TextMeshProUGUI _currentPage;
        [SerializeField] private TextMeshProUGUI _totalPages;

        private int _currentPageIndex = 1;
        private int TotalPageCount => _cardText.textInfo.pageCount;

        private void Start()
        {
            StartCoroutine(WaitForCardText(0.1f));
        }

        private IEnumerator WaitForCardText(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _cardText.pageToDisplay = _currentPageIndex;
            SetTextPageFields();
        }

        public void NextTextPage() 
        {
            int newPage = _currentPageIndex + 1;
            ChangeTextPage(newPage);
        }

        public void PreviousTextPage()
        {
            int newPage = _currentPageIndex - 1;
            ChangeTextPage(newPage);
        }
    
        private void ChangeTextPage(int pageIndex)
        {
            _currentPageIndex = Mathf.Clamp(pageIndex, 1, TotalPageCount);
            _cardText.pageToDisplay = _currentPageIndex;
            _currentPage.text = _currentPageIndex.ToString();
        }
    
        private void SetTextPageFields()
        {
            _currentPage.text = _currentPageIndex.ToString();
            _totalPages.text = TotalPageCount.ToString();
        }
    }
}
