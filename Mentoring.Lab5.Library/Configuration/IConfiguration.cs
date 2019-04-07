using System.Collections.Generic;
using System.Globalization;

namespace Mentoring.Lab5.Library.Configuration
{
    public interface IConfiguration
    {
        CultureInfo Culture { get; }
        IReadOnlyList<DirectoryConfiguration> Directories { get; }
        IReadOnlyList<RuleConfiguration> Rules { get; }
        RuleConfiguration DefaultRule { get; }
    }
}
