using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mentoring.Lab5.Library.Configuration;
using Mentoring.Lab5.Library.Models;
using Translations = Mentoring.Lab5.Library.Resources.Translations;

namespace Mentoring.Lab5.Library.Services
{
    public class FileWatcherService : IFileWatcherService
    {
        private readonly IReadOnlyList<FileSystemWatcher> _fileSystemWatchers;
        private readonly IFileService _fileService;
        private readonly ILoggerService _loggerService;

        public FileWatcherService(IConfiguration configuration,
            IFileService fileService,
            ILoggerService loggerService)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _fileSystemWatchers = configuration.Directories.Select(d => CreateFileSystemWatcher(d.Path))
                .ToList();
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }

        public void Start()
        {
            foreach (var fileSystemWatcher in _fileSystemWatchers)
            {
                fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        public void Dispose()
        {
            foreach (var fileSystemWatcher in _fileSystemWatchers)
            {
                fileSystemWatcher.Dispose();
            }
        }

        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            _loggerService.LogInfo(string.Format(Translations.FoundFile, e.FullPath));

            var fileModel = new FileModel
            {
                Name = e.Name,
                FullPath = e.FullPath
            };

            try
            {
                _fileService.MoveFile(fileModel);
            }
            catch (ArgumentNullException argumentNullException)
            {
                _loggerService.LogError(argumentNullException, Translations.Exception);
            }
            catch (ArgumentException argumentException)
            {
                _loggerService.LogError(argumentException, Translations.Exception);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                _loggerService.LogError(fileNotFoundException, Translations.Exception);
            }
            catch (IOException ioException)
            {
                _loggerService.LogError(ioException, Translations.Exception);
            }
            catch (Exception exception)
            {
                _loggerService.LogError(exception, Translations.Exception);
            }
        }

        private FileSystemWatcher CreateFileSystemWatcher(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException(nameof(directoryPath));
            }

            var fileSystemWatcher = new FileSystemWatcher(directoryPath)
            {
                NotifyFilter = NotifyFilters.FileName,
                IncludeSubdirectories = false
            };

            fileSystemWatcher.Created += FileSystemWatcherOnCreated;

            return fileSystemWatcher;
        }
    }
}
