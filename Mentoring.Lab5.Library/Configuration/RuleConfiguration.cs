namespace Mentoring.Lab5.Library.Configuration
{
    public class RuleConfiguration
    {
        public RuleConfiguration(string filePattern,
            string destinationDirectory,
            bool serialNumberEnable,
            bool movingDateEnable)
        {
            FilePattern = filePattern;
            DestinationDirectory = destinationDirectory;
            SerialNumberEnable = serialNumberEnable;
            MovingDateEnable = movingDateEnable;
        }

        public string FilePattern { get; }
        public string DestinationDirectory { get;  }
        public bool SerialNumberEnable { get; }
        public bool MovingDateEnable { get;}
    }
}
