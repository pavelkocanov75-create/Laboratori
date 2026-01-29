using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARLaboratory.Card
{
    [RequireComponent(typeof(Image))]
    public class TabGroupButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private TabGroup _tabGroup;
    
        public bool IsDisabled { get; private set; } = false;
        private Image _buttonImage;
        private readonly Color _disabledColor = new Color(165, 165, 165);

        private void Awake()
        {
            _buttonImage = GetComponent<Image>();
        }

        private void Start()
        {
            _tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _tabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tabGroup.OnTabExit(this);
        }

        public void DisableButton()
        {
            _buttonImage.color = _disabledColor;
            IsDisabled = true;
            _buttonImage.raycastTarget = false;
        }

        public void ChangeImageColor(Color color)
        {
            _buttonImage.color = color;
        }
    }
}
