using ARLaboratory.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARLaboratory.Core
{
    [RequireComponent(typeof(DragRotate))]
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _placeHolder;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _image;

        private static GameObject _itemPrefab;

        public void ShowCanvas()
        {
            gameObject.SetActive(true);
            _camera.gameObject.SetActive(true);
        }

        public void AssignData(string header, string description, Sprite sprite)
        {
            _header.text = header;
            _description.text = description;
            _image.sprite = sprite;
            _image.preserveAspect = true;
        }
    
        public void AssignRenderedModel(Equipment equipment)
        {
            if (_itemPrefab != null)
                Destroy(_itemPrefab);
        
            _itemPrefab = InstantiateRenderedModel(equipment);
        
            ResetLocalRotation(equipment);
        }
    
        private GameObject InstantiateRenderedModel(Equipment equipment)
        {
            GameObject showcaseItem = Instantiate(equipment.gameObject, _placeHolder.transform, true);
            SetObjectTransform(showcaseItem, equipment);
            return showcaseItem;
        }

        private void SetObjectTransform(GameObject showcaseItem, Equipment equipment)
        {
            Vector3 prefabLocalPosition = equipment.gameObject.transform.localPosition;
            showcaseItem.transform.localPosition = prefabLocalPosition;
            showcaseItem.transform.localRotation = Quaternion.identity;
            showcaseItem.transform.localScale *= equipment.ScaleFactor;
        }
    
        private void ResetLocalRotation(Equipment equipment) => _placeHolder.transform.localRotation = equipment.Rotation;
    }
}
