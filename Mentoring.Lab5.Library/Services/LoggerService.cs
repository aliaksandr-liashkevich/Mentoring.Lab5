using System;
using Translations = Mentoring.Lab5.Library.Resources.Translations;

namespace Mentoring.Lab5.Library.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogInfo(string message)
        {
            Console.WriteLine(Translations.LogInfo, message);
        }

        public void LogError(Exception exception, string message)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Console.WriteLine(Translations.LogError, message, exception.Message);
        }
    }
}
