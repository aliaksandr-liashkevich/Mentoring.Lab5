using System.Configuration;
using System.Globalization;

namespace Mentoring.Lab5.App.Configuration
{
    internal class SettingsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("culture", DefaultValue = "")]
        public CultureInfo Culture => (CultureInfo) this["culture"];

        [ConfigurationProperty("directories")]
        [ConfigurationCollection(typeof(DirectoryConfigurationElement), AddItemName = "directory")]
        public DirectoryConfigurationElementCollection Directories =>
            (DirectoryConfigurationElementCollection) this["directories"];

        [ConfigurationProperty("rules")]
        [ConfigurationCollection(typeof(RuleConfigurationElement), AddItemName = "rule")]
        public RuleConfigurationElementCollection Rules =>
            (RuleConfigurationElementCollection)this["rules"];
    }
}
