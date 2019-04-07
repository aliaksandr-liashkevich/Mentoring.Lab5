using System.Configuration;

namespace Mentoring.Lab5.App.Configuration
{
    internal class RuleConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("filePattern", IsKey = true)]
        public string FilePattern => (string) base["filePattern"];

        [ConfigurationProperty("destinationDirectory")]
        public string DestinationDirectory => (string) base["destinationDirectory"];

        [ConfigurationProperty("serialNumberEnable")]
        public bool SerialNumberEnable => (bool) base["serialNumberEnable"];

        [ConfigurationProperty("movingDateEnable")]
        public bool MovingDateEnable => (bool) base["movingDateEnable"];
    }
}
