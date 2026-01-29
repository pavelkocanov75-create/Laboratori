using ARLaboratory.Scriptable_Object;
using UnityEngine;

namespace ARLaboratory.Core
{
    public class EquipmentDataViewer : MonoBehaviour
    {
        public EquipmentUI UIEquipment;
        public Equipment Equipment;

        private EquipmentData _equipmentData;

        private void Start()
        {
            _equipmentData = Equipment.EquipmentData;
        }

        public void DisplayEquipmentData()
        {
            UIEquipment.ShowCanvas();
            UIEquipment.AssignData(_equipmentData.Header, _equipmentData.Description, _equipmentData.Sprite);
            UIEquipment.AssignRenderedModel(Equipment);
        }
    }
}
