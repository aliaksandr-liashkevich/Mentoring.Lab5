using System;
using System.Collections.Generic;
using Mentoring.Lab5.App.Configuration;
using Mentoring.Lab5.Library.Configuration;

namespace Mentoring.Lab5.App.Extensions
{
    internal static class SettingExtensions
    {
        public static IConfiguration ConvertToConfiguration(this SettingsConfigurationSection settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var directories = new List<DirectoryConfiguration>();

            foreach (DirectoryConfigurationElement settingsDirectory in settings.Directories)
            {
                directories.Add(new DirectoryConfiguration(settingsDirectory.Path));
            }

            var rules = new List<RuleConfiguration>();

            foreach (RuleConfigurationElement rule in settings.Rules)
            {
                rules.Add(new RuleConfiguration(rule.FilePattern,
                    rule.DestinationDirectory,
                    rule.SerialNumberEnable,
                    rule.MovingDateEnable));
            }

            var defaultRule = new RuleConfiguration("*",
                settings.Rules.DefaultDirectoryPath,
                true,
                true);

            return new Config(settings.Culture,
                directories,
                rules,
                defaultRule);
        }
    }
}
