using System.IO;
using Microsoft.Extensions.Configuration;

namespace Dovebot.Services
{
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; set; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}