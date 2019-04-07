using System.Configuration;

namespace Mentoring.Lab5.App.Configuration
{
    internal class DirectoryConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true)]
        public string Path => (string) base["path"];
    }
}
