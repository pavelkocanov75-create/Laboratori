using UnityEngine;
using System.Collections;
using TMPro;

// Get WebCam information from the browser
public class ExampleClass : MonoBehaviour
{
    private WebCamDevice[] _devices;

    public TextMeshProUGUI Textfield_1;
    public TextMeshProUGUI Textfield_2;
    
    // Use this for initialization
    private IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("webcam found");
            _devices = WebCamTexture.devices;
            foreach (WebCamDevice device in _devices)
            {
                Debug.Log("devices[cameraIndex].name: ");
                Textfield_1.text = device.name;
                Debug.Log("devices[cameraIndex].isFrontFacing");
                Textfield_2.text = device.isFrontFacing.ToString();
            }
        }
        else
        {
            Debug.Log("no webcams found");
        }
    }
}