using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace _5ERAT11.Pages
{
    public abstract class AbstractPage
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;

        protected AbstractPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, System.TimeSpan.FromSeconds(15));
        }
    }
}
