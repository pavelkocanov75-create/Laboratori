using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLabel;
    
    private readonly WaitForSeconds _waitForSeconds= new (0.1f);
    private float _count;
    
    private IEnumerator Start()
    {
        while (true)
        {
            _count = 1f / Time.unscaledDeltaTime;
            _textLabel.text = $"FPS: {Mathf.Round(_count).ToString()}";
            yield return _waitForSeconds;
        }
    }
}
