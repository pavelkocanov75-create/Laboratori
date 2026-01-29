using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARLaboratory.Card
{
    public class ImageGalleryButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
    
        public Canvas GalleryCanvas;
        public Image Image => _image;

        private ImageGalleryControl _imageGalleryControl;

        private void Start()
        {
            _imageGalleryControl = GalleryCanvas.GetComponent<ImageGalleryControl>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _imageGalleryControl.OnImageButtonSelected(this);
        }
    }
}
