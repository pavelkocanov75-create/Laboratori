using ARLaboratory.Scriptable_Object;
using UnityEngine;

namespace ARLaboratory.Core
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private EquipmentData _equipmentData;

        [Header("Equipment UI Transformation")] [Tooltip("Leave at zero for using original object transformation")] 
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _scaleFactor = 1f;

        public EquipmentData EquipmentData => _equipmentData;
        public Quaternion Rotation => Quaternion.Euler(_rotation);
        public float ScaleFactor => _scaleFactor;
    }
}
