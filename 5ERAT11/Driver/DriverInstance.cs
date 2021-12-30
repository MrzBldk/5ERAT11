using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using _5ERAT11.Services;

namespace _5ERAT11.Driver
{
    class DriverInstance
    {
        private static IWebDriver driver;
        public static IWebDriver GetInstance()
        {
            if (driver == null)
            {
                switch (TestDataReader.GetSettings().BrowserSetting.BrowserName)
                {
                    case "chrome":
                        new DriverManager().SetUpDriver(new ChromeConfig(), TestDataReader.GetSettings().BrowserSetting.BrowserVersion);
                        driver = new ChromeDriver();
                        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                        driver.Manage().Window.Maximize(); 
                        break;
                    case "firefox":
                        new DriverManager().SetUpDriver(new FirefoxConfig(), TestDataReader.GetSettings().BrowserSetting.BrowserVersion);
                        driver = new FirefoxDriver();
                        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                        driver.Manage().Window.Maximize();
                        break;
                    case "msedge":
                        new DriverManager().SetUpDriver(new EdgeConfig(), TestDataReader.GetSettings().BrowserSetting.BrowserVersion);
                        driver = new EdgeDriver();
                        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                        driver.Manage().Window.Maximize();
                        break;
                    case "opera":
                        new DriverManager().SetUpDriver(new OperaConfig(), TestDataReader.GetSettings().BrowserSetting.BrowserVersion);
                        driver = new OperaDriver();
                        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                        driver.Manage().Window.Maximize();
                        break;
                    case "IE":
                        new DriverManager().SetUpDriver(new InternetExplorerConfig(), TestDataReader.GetSettings().BrowserSetting.BrowserVersion);
                        driver = new InternetExplorerDriver();
                        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                        driver.Manage().Window.Maximize();
                        break;
                }

            }
            return driver;
        }

        public static void CloseBrowser()
        {
            driver.Quit();
            driver = null;
        }
    }
}
