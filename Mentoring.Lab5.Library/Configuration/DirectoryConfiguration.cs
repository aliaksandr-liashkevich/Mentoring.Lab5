namespace Mentoring.Lab5.Library.Configuration
{
    public class DirectoryConfiguration
    {
        public DirectoryConfiguration(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
