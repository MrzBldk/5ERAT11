using System.Reflection;
using System.IO;
using Microsoft.Extensions.Configuration;
using _5ERAT11.Models;

namespace _5ERAT11.Services
{
    class TestDataReader
    {
        public static Settings GetSettings()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.FullName)
                .AddJsonFile("Resources/data.json").Build();

            var section = config.GetSection(nameof(Settings));

            var testsSettings = section.Get<Settings>();

            return testsSettings;
        }
    }
}
