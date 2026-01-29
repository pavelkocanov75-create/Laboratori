namespace ARLaboratory.Loggers
{
    public class NullLogger : ILogger
    {
        public static NullLogger Instance => _instance ??= new NullLogger();
    
        private static NullLogger _instance;
        public void Log(object message)
        {
        
        }

        public void LogError(object message)
        {

        }
    }
}