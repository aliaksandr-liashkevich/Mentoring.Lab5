using System.Configuration;

namespace Mentoring.Lab5.App.Configuration
{
    internal class DirectoryConfigurationElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryConfigurationElement) element).Path;
        }
    }
}
