using System;
using System.IO;
using System.Reflection;
using Serilog;


namespace _5ERAT11.Utils
{
    public static class Log
    {
        public static ILogger log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.FullName +
                    "/SavedLogs/SavedLogInformation.log")
            .CreateLogger();

        public static void Info(string message)
        {
            log.Information(message);
        }

        public static void Info(Exception ex, string message)
        {
            log.Information(ex, message);
        }
    }
}
