using ARLaboratory;
using ARLaboratory.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    public TextMeshProUGUI Label => _label;
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        EquipmentUIControl.ItemIndex = transform.GetSiblingIndex();
    }
}
