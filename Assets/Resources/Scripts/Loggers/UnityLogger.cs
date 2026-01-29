using UnityEngine;

namespace ARLaboratory.Loggers
{
    public class UnityLogger : ILogger
    {
        public void Log(object message)
        {
            Debug.Log(message);
        }

        public void LogError(object message)
        {
            Debug.LogError(message);
        }
    }
}
