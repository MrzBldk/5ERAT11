using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using _5ERAT11.Driver;

namespace _5ERAT11.Utils
{
    public class ScreenShot
    {
        public static void MakeScreenShot()
        {
            Screenshot ss = ((ITakesScreenshot)DriverInstance.GetInstance()).GetScreenshot();
            string path = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");

            try
            {
                ss.SaveAsFile(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.FullName +
                    "/SavedLogs/" + path + ".png", ScreenshotImageFormat.Png);

                Log.Info("Screenshot was taken");
            }
            catch (Exception e)
            {
                Log.Info(e, "Screenshot wasn't taken");
                throw;
            }
        }
    }
}