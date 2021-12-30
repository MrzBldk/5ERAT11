using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;

namespace _5ERAT11.Pages
{
    public class RoboForexPage:AbstractPage
    {
        public IWebElement Body => _wait.Until(_driver => _driver.FindElement(By.CssSelector("body")));
        public IWebElement AllowCockiesButton => _wait.Until(_driver => _driver.FindElement(By.Id("AllowCookies_Allow_ViewButton")));
        public IWebElement LoginInput => _wait.Until(_driver => _driver.FindElement(By.XPath("//input[contains(@type,'email')]")));
        public IWebElement PasswordInput => _wait.Until(_driver => _driver.FindElement(By.XPath("//input[contains(@type,'password')]")));
        public IWebElement LoginButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".authentication-form__button > .button")));
        public IWebElement QuickDealInput => _wait.Until(_driver => _driver.FindElement(By.Id("QuickTrading_VolumesList_NumberInput_InputView")));
        public IWebElement QuickSellButton => _wait.Until(_driver => _driver.FindElement(By.Id("QuickTrading_Sell_ViewButton")));
        public IWebElement QuickBuyButton => _wait.Until(_driver => _driver.FindElement(By.Id("QuickTrading_Buy_ViewButton")));
        public IWebElement OrderType => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".grid__side.grid__cell > .grid__side-wrap.grid__wrap-in")));
        public IWebElement OrderSize => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".grid__volume.grid__cell > .grid__volume-wrap.grid__wrap-in")));
        public IWebElement OrderSymbol => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".grid__symbol.grid__cell > .symbol-button")));
        public IWebElement CloseAllButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#margin-info-panel > div.auth-button-wrapper")));
        public IWebElement PositionCounter => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#positions-tab > div.items-counter > span")));
        public IWebElement ColorChandgeButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector("div.chart-cntxt-menu__color-scheme > div")));
        public IWebElement Canvas => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".cnvs")));
        public IWebElement LanguageSelector => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".lang-selector")));
        public IWebElement RussianLanguageSelector => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".drop-down__item_lang_ru")));
        public IWebElement ModalWindowTitle => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".modal-window__title")));
        public IWebElement NewOrderButton => _wait.Until(_driver => _driver.FindElement(By.Id("new-order_ViewButton")));
        public IWebElement SellStopButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Sell Stop']")));
        public IWebElement BuyStopButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Buy Stop']")));
        public IWebElement SellLimitButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Sell Limit']")));
        public IWebElement BuyLimitButton => _wait.Until(_driver => _driver.FindElement(By.XPath("//div[text()='Buy Limit']")));
        public IWebElement PendingOrderButton => _wait.Until(_driver => _driver.FindElement(By.Id("PendingOrder_OpenOrder_ViewButton")));
        public IWebElement CloseWindowButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".modal-window__close")));
        public IWebElement PendingOrders => _wait.Until(_driver => _driver.FindElements(By.CssSelector(".tabs__item"))[2]);
        public IWebElement Positions => _wait.Until(_driver => _driver.FindElement(By.Id("positions-tab")));
        public IWebElement PendingOrderType => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".grid__body .grid__row .grid__type-wrap")));
        public IWebElement PendingOrderInput => _wait.Until(_driver => _driver.FindElement(By.Id("PendingOrder_NumberInput_InputView")));


        public RoboForexPage(IWebDriver driver) : base(driver) { }

        public RoboForexPage OpenPage()
        {
            _driver.Navigate().GoToUrl("https://webtrader.roboforex.com/");
            return this;
        }

        public RoboForexPage AllowCockies()
        {
            AllowCockiesButton.Click();
            return this;
        }

        public RoboForexPage Login(string login, string password)
        {
            LoginInput.SendKeys(login);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
            return this;
        }

        public RoboForexPage SellBTC(string amount)
        {
            Positions.Click();
            QuickDealInput.SendKeys(Keys.Backspace);
            QuickDealInput.SendKeys(amount);
            QuickSellButton.Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__side.grid__cell > .grid__side-wrap.grid__wrap-in"), "Sell"));
            return this;
        }

        public RoboForexPage BuyBTC(string amount)
        {
            Positions.Click();
            QuickDealInput.SendKeys(Keys.Backspace);
            QuickDealInput.SendKeys(amount);
            QuickBuyButton.Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__side.grid__cell > .grid__side-wrap.grid__wrap-in"), "Buy"));
            return this;
        }

        public RoboForexPage CloseAllPositions()
        {
            Positions.Click();
            CloseAllButton.Click();
            _wait.Until(driver =>
            {
                IWebElement tempElement = _driver.FindElement(By.CssSelector("#positions-tab > div.items-counter > span"));
                return !tempElement.Displayed ? tempElement : null;
            });
            return this;
        }

        public RoboForexPage ChangeColorScheme()
        {
            Actions actions = new Actions(_driver);
            actions.ContextClick(Canvas).Perform();
            ColorChandgeButton.Click();
            Body.Click();
            return this;
        }

        public RoboForexPage ChangeLanguageToRussian()
        {
            LanguageSelector.Click();
            RussianLanguageSelector.Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".modal-window__title"), "Авторизация"));
            return this;
        }
        public RoboForexPage CreateOrder()
        {
            PendingOrders.Click();
            NewOrderButton.Click();
            return this;
        }
        public RoboForexPage CreateSellStopOrder()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", SellStopButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys("1");
            PendingOrderButton.Click();
            CloseWindowButton.Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Sell Stop"));
            return this;
        }

        public RoboForexPage CreateBuyStopOrder()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", BuyStopButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys("9999999");
            PendingOrderButton.Click();
            CloseWindowButton.Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Buy Stop"));
            return this;
        }

        public RoboForexPage CreateSellLimitOrder()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", SellLimitButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys("9999999");
            PendingOrderButton.Click();
            CloseWindowButton.Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Sell Limit"));
            return this;
        }

        public RoboForexPage CreateBuyLimitOrder()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", BuyLimitButton);
            for (var i = 0; i < 10; i++)
                PendingOrderInput.SendKeys(Keys.Backspace);
            PendingOrderInput.SendKeys("1");
            PendingOrderButton.Click();
            CloseWindowButton.Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__body .grid__row .grid__type-wrap"), "Buy Limit"));
            return this;
        }
    }
}

