using ARLaboratory.Scriptable_Object;
using TMPro;
using UnityEngine;

namespace ARLaboratory.CanvasControl
{
    public class EquipmentLabel : MonoBehaviour
    {
        [SerializeField] private EquipmentData equipmentData;
        [SerializeField] private TextMeshProUGUI label;

        [Header("Overwrite Label Name")]
        [Tooltip("Overwrites scriptable object's name. Leave it blank if you want to preserve the original name.")]
        [SerializeField] private string newName;

        private void Start()
        {
            AssignLabelText();
        }

        private void AssignLabelText()
        {
            label.text = newName == "" ? equipmentData.Header : newName;
        }
    }
}

