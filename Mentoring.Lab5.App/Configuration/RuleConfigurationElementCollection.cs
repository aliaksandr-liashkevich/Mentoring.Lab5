using System.Configuration;

namespace Mentoring.Lab5.App.Configuration
{
    internal class RuleConfigurationElementCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultDirectoryPath")]
        public string DefaultDirectoryPath => (string) this["defaultDirectoryPath"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleConfigurationElement) element).FilePattern;
        }
    }
}
