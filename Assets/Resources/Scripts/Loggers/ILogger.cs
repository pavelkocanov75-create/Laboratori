
namespace ARLaboratory.Loggers
{
    public interface ILogger
    {
        void Log(object message);

        void LogError(object message);
    }
}
