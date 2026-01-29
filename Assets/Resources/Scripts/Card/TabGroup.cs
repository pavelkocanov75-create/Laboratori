using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARLaboratory.Card
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private Color _idleTabsColor;
    
        [SerializeField] private List<TabGroupButton> _tabButtons;
        [SerializeField] private List<GameObject> _pages;
        [SerializeField] private List<GameObject> _controlPanels;

        private static TabGroupButton _selectedTab;
        private Color _selectedColor;

        private void Start()
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                SelectActiveTabButton(i);
            }
            ResetTabs();
        }

        private void SelectActiveTabButton(int i)
        {
            bool pageDisabled = !_pages[i].activeSelf;
            if (pageDisabled) return;
            _selectedTab = _tabButtons[i];
            _selectedColor = _selectedTab.gameObject.GetComponent<Image>().color;
        }

        public void Subscribe(TabGroupButton button)
        {
            if (_tabButtons != null) return;
            _tabButtons = new List<TabGroupButton>();
            _tabButtons.Add(button);
        }

        public void OnTabEnter(TabGroupButton button)
        {
            ResetTabs();
            if (_selectedTab == null || button != _selectedTab)
                button.ChangeImageColor(_selectedColor);
        }

        public void OnTabExit(TabGroupButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabGroupButton button)
        {
            _selectedTab = button;
            ResetTabs();
            button.ChangeImageColor(_selectedColor);
            SetActivePage(button);
        }

        private void SetActivePage(TabGroupButton button)
        {
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < _pages.Count; i++)
            {
                _pages[i].SetActive(i == index);
                _controlPanels[i].SetActive(i == index);
            }
        }

        private void ResetTabs()
        {
            foreach(TabGroupButton button in _tabButtons)
            {
                if (IsSelectedButton(button) || button.IsDisabled)
                    continue;

                button.ChangeImageColor(_idleTabsColor);
            }
        }

        private bool IsSelectedButton(TabGroupButton button) => _selectedTab != null && button == _selectedTab;
    }
}
