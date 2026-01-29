using UnityEngine;

namespace ARLaboratory.Core
{
    [RequireComponent(typeof(EquipmentUI))]
    public class EquipmentUIControl : MonoBehaviour
    {
        [SerializeField] private GameObject[] _scrollViewContents;

        public static int ItemIndex;

        private GameObject _content;
        private int _childCount = 0;

        private void OnEnable()
        {
            SelectActiveContent();
            _childCount = _content.transform.childCount;
        }

        public void PreviousItem()
        {
            ItemIndex -= 1;

            if (ItemIndex < 0) 
                ItemIndex = _childCount - 1;

            ChangeItem();
        }

        public void NextItem()
        {
            ItemIndex += 1;

            if (ItemIndex >= _childCount)
                ItemIndex = 0;

            ChangeItem();
        }

        private void ChangeItem()
        {
            GameObject item = _content.transform.GetChild(ItemIndex).gameObject;
            EquipmentDataViewer newRenderData = item.GetComponent<EquipmentDataViewer>();
            newRenderData.DisplayEquipmentData();
        }

        private void SelectActiveContent()
        {
            foreach (var content in _scrollViewContents)
            {
                if (!content.activeInHierarchy) 
                    continue;
            
                _content = content;
                break;
            }
        }
    }
}
