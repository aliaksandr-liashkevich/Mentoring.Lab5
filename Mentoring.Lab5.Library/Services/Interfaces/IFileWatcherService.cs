using System;

namespace Mentoring.Lab5.Library.Services
{
    public interface IFileWatcherService : IDisposable
    {
        void Start();
    }
}
