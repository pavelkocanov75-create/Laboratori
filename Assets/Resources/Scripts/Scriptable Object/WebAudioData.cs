using UnityEngine;

namespace ARLaboratory.Scriptable_Object
{
    [CreateAssetMenu(fileName = "New Web Audio", menuName = "Web Audio Data")]
    public class WebAudioData : ScriptableObject
    {
        [SerializeField] private string _audioUrl;
        public string AudioUrl => _audioUrl;
    }
}
