using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using _5ERAT11.Utils;

namespace _5ERAT11.Elements
{
    public class CreateOrderModalWindow : AbstractElement
    {
        public CreateOrderModalWindow(IWebDriver driver) : base(driver) { }
        public IWebElement NewOrderButton => _wait.Until(_driver => _driver.FindElement(By.Id("new-order_ViewButton")));
        public IWebElement SellStopButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Sell Stop']")));
        public IWebElement BuyStopButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Buy Stop']")));
        public IWebElement SellLimitButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Sell Limit']")));
        public IWebElement BuyLimitButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Buy Limit']")));
        public IWebElement PendingOrderButton => _wait.Until(_driver => _driver.FindElement(By.Id("PendingOrder_OpenOrder_ViewButton")));
        public IWebElement CloseWindowButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".modal-window__close")));
        public IWebElement PendingOrders => _wait.Until(_driver => _driver.FindElements(By.CssSelector(".tabs__item"))[2]);
        public IWebElement PendingOrderType => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".grid__body .grid__row .grid__type-wrap")));
        public IWebElement PendingOrderPrice => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".grid__body .grid__row .grid__entry_price-wrap")));
        public IWebElement PendingOrderSymbol => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".grid__body .grid__row .symbol-button")));

        //symbol-button
        public IWebElement PendingOrderInput => _wait.Until(_driver => _driver.FindElement(By.Id("PendingOrder_NumberInput_InputView")));
        public CreateOrderModalWindow OpenOrderCreatingWindow()
        {
            PendingOrders.Click();
            NewOrderButton.Click();
            Log.Info("Modal Window opened");
            return this;
        }
        public CreateOrderModalWindow CreateSellStopOrder(string price)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", SellStopButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys(price);
            PendingOrderButton.Click();
            Log.Info("Pending order opened");
            CloseWindowButton.Click();
            Log.Info("Modal window closed");
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Sell Stop"));
            return this;
        }

        public CreateOrderModalWindow CreateBuyStopOrder(string price)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", BuyStopButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys(price);
            PendingOrderButton.Click();
            Log.Info("Pending order opened");
            CloseWindowButton.Click();
            Log.Info("Modal window closed");
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Buy Stop"));
            return this;
        }

        public CreateOrderModalWindow CreateSellLimitOrder(string price)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", SellLimitButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys(price);
            PendingOrderButton.Click();
            Log.Info("Pending order opened");
            CloseWindowButton.Click();
            Log.Info("Modal window closed");
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Sell Limit"));
            return this;
        }

        public CreateOrderModalWindow CreateBuyLimitOrder(string price)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", BuyLimitButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys(price);
            PendingOrderButton.Click();
            Log.Info("Pending order opened");
            CloseWindowButton.Click();
            Log.Info("Modal window closed");
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Buy Limit"));
            return this;
        }
    }
}
