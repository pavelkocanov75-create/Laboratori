using UnityEngine;
using UnityEngine.Events;

public class WebDownloadHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDownloadFailed;

    private IDownloadable _webDownloader;

    private void Awake()
    {
        _webDownloader = GetComponent<IDownloadable>();
        if (_webDownloader == null) return;
        _webDownloader.DownloadFailed += WebDownloadFailed;
    }

    private void WebDownloadFailed()
    {
        OnDownloadFailed?.Invoke();
        
        _webDownloader.DownloadFailed -= WebDownloadFailed;
    }

    private void OnDestroy() 
    {
        _webDownloader.DownloadFailed -= WebDownloadFailed;
    }
}
