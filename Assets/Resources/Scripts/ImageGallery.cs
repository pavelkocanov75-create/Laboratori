using System;
using System.Collections.Generic;
using ARLaboratory;
using ARLaboratory.Card;
using ARLaboratory.Core;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WebImageDownloader))]
public class ImageGallery : AbstractLoadable
{
    [SerializeField] private ImageGalleryButton _imageButtonPrefab;
    [SerializeField] private GameObject _imageGalleryPage;
    [SerializeField] private Canvas _imageCanvas;

    public event Action ImageGalleryCreated;
    
    private WebImageDownloader _webImageDownloader;
    private bool _isGalleryCreated = false;

    private void Awake()
    {
        _webImageDownloader = GetComponent<WebImageDownloader>();
    }

    private void Start()
    {
        _webImageDownloader.ImageDownloadFinished += CreateImageGallery;
    }
    
    public override bool IsDone() => _isGalleryCreated;
    
    private void CreateImageGallery()
    {
        List<Texture2D> textures = _webImageDownloader.DownloadedImages;
        foreach (Texture2D texture in textures)
            CreateImageObject(texture);

        OnImageDownloadFinished();
    }

    private void OnImageDownloadFinished()
    {
        ImageGalleryCreated?.Invoke();
        
        _isGalleryCreated = true;
        
        _webImageDownloader.ImageDownloadFinished -= CreateImageGallery;
    }

    private void CreateImageObject(Texture2D texture)
    {
        Rect rectangular = new(0, 0, texture.width, texture.height);
        ImageGalleryButton imageButton = CreateImageButton();
        imageButton.GalleryCanvas = _imageCanvas;
        AssignSpriteToImage(texture, imageButton, rectangular);
    }

    private static void AssignSpriteToImage(Texture2D texture, ImageGalleryButton imageButton, Rect rectangular)
    {
        Image image = imageButton.Image;
        image.color = Color.white;
        image.sprite = Sprite.Create(texture, rectangular, Vector2.one / 2);
    }

    private ImageGalleryButton CreateImageButton()
    {
        var buttonObject = Instantiate(_imageButtonPrefab.gameObject, _imageGalleryPage.transform, false);
        var imageButton = buttonObject.GetComponent<ImageGalleryButton>();
        return imageButton;
    }


    private void OnDestroy()
    {
        _webImageDownloader.ImageDownloadFinished -= CreateImageGallery;
    }
}
