using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using Mentoring.Lab5.App.Configuration;
using Mentoring.Lab5.App.Extensions;
using Mentoring.Lab5.Library.Services;

namespace Mentoring.Lab5.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new LoggerService();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                logger.LogInfo("Exit");
                Environment.Exit(0);
            };

            var settings = (SettingsConfigurationSection)ConfigurationManager.GetSection("settings");

            CultureInfo.DefaultThreadCurrentCulture = settings.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = settings.Culture;
            CultureInfo.CurrentUICulture = settings.Culture;
            CultureInfo.CurrentCulture = settings.Culture;

            var configuration = settings.ConvertToConfiguration();
            var fileService = new FileService(configuration, logger);

            using (var fileWatcherService = new FileWatcherService(configuration, fileService, logger))
            {
                fileWatcherService.Start();

                while (true)
                {
                    Console.ReadKey();
                }
            }
        }
    }
}
