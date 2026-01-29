using TMPro;
using UnityEngine;

namespace ARLaboratory.Loggers
{
    public class TextTMPLogger : ILogger
    {
        private readonly TextMeshProUGUI _label;
        public TextTMPLogger(TextMeshProUGUI label)
        {
            _label = label;
        }
    
        public void Log(object message)
        {
            _label.text += $"LOG: {message}/n";
        }

        public void LogError(object message)
        {
            _label.color = Color.red;
            _label.text += $"ERROR: {message}\n";
        }
    }
}