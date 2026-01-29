using UnityEngine;

namespace ARLaboratory.Scriptable_Object
{
    [CreateAssetMenu(fileName = "New Equipment Data", menuName = "Equipment Data")]
    public class EquipmentData : ScriptableObject {
    
        [SerializeField] private string _header;

        [TextArea(1, 20)] 
        [SerializeField] private string _description;

        [SerializeField] private Sprite _sprite;

        public string Header => _header;
        public string Description => _description;
        public Sprite Sprite => _sprite;
    }
}

