using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Mentoring.Lab5.Library.Configuration;
using Mentoring.Lab5.Library.Models;
using Mentoring.Lab5.Library.Resources;

namespace Mentoring.Lab5.Library.Services
{
    public class FileService : IFileService
    {
        private readonly RuleConfiguration _defaultRule;
        private readonly IReadOnlyList<RuleConfiguration> _rulesConfiguration;
        private readonly ILoggerService _loggerService;

        public FileService(IConfiguration configuration,
            ILoggerService loggerService)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _defaultRule = configuration.DefaultRule ?? throw new ArgumentException(Translations.DefaultRuleConfigurationNotFound, nameof(configuration));
            _rulesConfiguration = configuration.Rules;
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }

        public void MoveFile(FileModel file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var rule = _rulesConfiguration.FirstOrDefault(r => Regex.IsMatch(file.Name, r.FilePattern));

            if (rule != null)
            {
                _loggerService.LogInfo(string.Format(Translations.RuleFound, file.Name));
            }
            else
            {
                _loggerService.LogInfo(string.Format(Translations.RuleNotFound, file.Name));
            }

            rule = rule ?? _defaultRule;

            var sourceFilePath = file.FullPath;
            var destinationFilePath = CreateDestinationFilePath(rule, file);

            MoveFile(sourceFilePath, destinationFilePath);
        }

        private string CreateDestinationFilePath(RuleConfiguration rule, FileModel file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.Name);

            if (fileName == null)
            {
                throw new ArgumentException(string.Format(Translations.FileNameNotFound, file.FullPath),
                    nameof(file));
            }

            var builder = new StringBuilder();

            var destinationFileName = Path.Combine(rule.DestinationDirectory, fileName);
            builder.Append(destinationFileName);

            if (rule.MovingDateEnable)
            {
                var currentDateTime = DateTime.UtcNow;
                builder.Append($"_{currentDateTime.ToLocalTime():yy-MM-dd}");
            }

            if (rule.SerialNumberEnable)
            {
                var newGuid = Guid.NewGuid().ToString();
                builder.Append($"_{newGuid}");
            }

            var fileExtension = Path.GetExtension(file.FullPath);
            builder.Append(fileExtension);

            return builder.ToString();
        }

        private void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            if (File.Exists(destinationFilePath))
            {
                throw new ArgumentException(string.Format(Translations.FileAlreadyExists, destinationFilePath),
                    nameof(destinationFilePath));
            }

            var destinationDirectoryPath = Path.GetDirectoryName(destinationFilePath);

            if (destinationDirectoryPath == null)
            {
                throw new ArgumentException(string.Format(Translations.DirectoryPathNotFound, destinationFilePath),
                    nameof(destinationDirectoryPath));
            }

            if (!Directory.Exists(destinationDirectoryPath))
            {
                Directory.CreateDirectory(destinationDirectoryPath);
            }

            File.Move(sourceFilePath, destinationFilePath);
        }
    }
}
