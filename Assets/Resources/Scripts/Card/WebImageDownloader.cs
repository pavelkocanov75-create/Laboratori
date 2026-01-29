using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARLaboratory.Scriptable_Object;
using UnityEngine;
using UnityEngine.Networking;

namespace ARLaboratory.Card
{
    public class WebImageDownloader : MonoBehaviour, IDownloadable
    {
        [SerializeField] private ExperimentManager _experimentManager;
        [SerializeField] private int _loadingTimeout = 8;
    
        public event Action ImageDownloadFinished;
        public event Action DownloadFailed;

        public List<Texture2D> DownloadedImages { get; private set; }

        private void Awake()
        {
            DownloadedImages = new List<Texture2D>();
            var imageLinks = _experimentManager.CurrentExperiment.ImageLinks;
            StartCoroutine(DownloadWebImages(imageLinks.Where(link => !String.IsNullOrEmpty(link))));
        }

        private IEnumerator DownloadWebImages(IEnumerable<string> imageLinks)
        {
            foreach (string url in imageLinks)
            {
                UnityWebRequest loader = UnityWebRequestTexture.GetTexture(url);
                loader.timeout = _loadingTimeout;
                yield return loader.SendWebRequest();
                if (loader.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning($"Image {url} hasn't been downloaded due the error. Please, check the link and try again.");
                    continue;
                }

                var texture = DownloadHandlerTexture.GetContent(loader);
                DownloadedImages.Add(texture);
            }

            if (DownloadedImages.Count == 0)
                DownloadFailed?.Invoke();

            ImageDownloadFinished?.Invoke();
        }
    }
}
