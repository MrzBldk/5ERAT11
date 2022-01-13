using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace _5ERAT11.Elements
{
    public abstract class AbstractElement
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;

        protected AbstractElement(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, System.TimeSpan.FromSeconds(15));
        }
    }
}
