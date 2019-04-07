using System;

namespace Mentoring.Lab5.Library.Services
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(Exception exception, string message);
    }
}
