using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mentoring.Lab5.Library.Configuration
{
    public class Config : IConfiguration
    {
        public Config(CultureInfo culture,
            IReadOnlyList<DirectoryConfiguration> directories,
            IReadOnlyList<RuleConfiguration> rules,
            RuleConfiguration defaultRule)
        {
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
            Directories = directories ?? throw new ArgumentNullException(nameof(directories));
            Rules = rules ?? throw new ArgumentNullException(nameof(rules));
            DefaultRule = defaultRule ?? throw new ArgumentNullException(nameof(defaultRule));
        }

        public CultureInfo Culture { get; }
        public IReadOnlyList<DirectoryConfiguration> Directories { get; }
        public IReadOnlyList<RuleConfiguration> Rules { get; }
        public RuleConfiguration DefaultRule { get; }
    }
}
