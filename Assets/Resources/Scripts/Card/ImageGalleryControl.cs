using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARLaboratory.Card
{
    public class ImageGalleryControl : MonoBehaviour
    {
        [SerializeField] private Image _canvasImage;
        [SerializeField] private GameObject _galleryPage;

        private ImageGallery _imageGallery;
        private List<ImageGalleryButton> _imagesButtonsList;
        private Canvas _canvas;
        private ImageGalleryButton _selectedImageButton;
        private int _selectedImageIndex;


        private void Awake()
        {
            _imageGallery = GetComponent<ImageGallery>();
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            _imageGallery.ImageGalleryCreated += GetImageGalleryList;
        }
    
        private void GetImageGalleryList()
        {
            CreateImageButtonList();
        
            _imageGallery.ImageGalleryCreated -= GetImageGalleryList;
        }

        private void CreateImageButtonList()
        {
            _imagesButtonsList = new();
            foreach (Transform child in _galleryPage.transform)
            {
                var imageButton = child.GetComponent<ImageGalleryButton>();
                _imagesButtonsList.Add(imageButton);
            }
        }

        private int GetSelectedImageIndex() => _selectedImageButton.transform.GetSiblingIndex();

        public void OnImageButtonSelected(ImageGalleryButton imageButton)
        {
            _selectedImageButton = imageButton;
            int selectedImageIndex = GetSelectedImageIndex();
            for (int i = 0; i < _imagesButtonsList.Count; i++)
            {
                bool isSelectedImage = i == selectedImageIndex;
                if (isSelectedImage) 
                    ShowSelectedImageOnCanvas(i);
            }
        }

        public void PreviousImage() {
            int index = GetSelectedImageIndex();
            int prevIndex = index - 1;
        
            if (prevIndex < 0)
                prevIndex = _imagesButtonsList.Count - 1;
        
            ChangeImage(prevIndex);
        }

        public void NextImage() {
            int index = GetSelectedImageIndex();
            int nextIndex = index + 1;
        
            if (nextIndex >= _imagesButtonsList.Count) 
                nextIndex = 0;

            ChangeImage(nextIndex);
        }

        private void ChangeImage(int index)
        {
            ImageGalleryButton newImageButton = _imagesButtonsList[index];
            _canvasImage.sprite = newImageButton.Image.sprite;
            _selectedImageButton = newImageButton;
        }

        private void ShowSelectedImageOnCanvas(int index)
        {
            _canvasImage.sprite = _imagesButtonsList[index].Image.sprite;
            _canvasImage.preserveAspect = true;
            _canvas.enabled = true;
        }

        private void OnDestroy()
        {
            _imageGallery.ImageGalleryCreated -= GetImageGalleryList;
        }
    }
}
