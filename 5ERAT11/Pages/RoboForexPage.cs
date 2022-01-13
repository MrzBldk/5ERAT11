using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using _5ERAT11.Utils;
using _5ERAT11.Elements;

namespace _5ERAT11.Pages
{
    public class RoboForexPage : AbstractPage
    {
        public IWebElement Body => _wait.Until(_driver => _driver.FindElement(By.CssSelector("body")));
        public IWebElement AllowCookiesButton => _wait.Until(_driver => _driver.FindElement(By.Id("AllowCookies_Allow_ViewButton")));
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
        public IWebElement Positions => _wait.Until(_driver => _driver.FindElement(By.Id("positions-tab")));


        public CreateOrderModalWindow CreateOrderModalWindow;


        public RoboForexPage(IWebDriver driver) : base(driver)
        {
            CreateOrderModalWindow = new CreateOrderModalWindow(driver);
        }

        public RoboForexPage OpenPage()
        {
            _driver.Navigate().GoToUrl("https://webtrader.roboforex.com/");
            return this;
        }

        public RoboForexPage AllowCookies()
        {
            AllowCookiesButton.Click();
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
            Log.Info("BTC sold");
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__side.grid__cell > .grid__side-wrap.grid__wrap-in"), "Sell"));
            return this;
        }

        public RoboForexPage BuyBTC(string amount)
        {
            Positions.Click();
            QuickDealInput.SendKeys(Keys.Backspace);
            QuickDealInput.SendKeys(amount);
            QuickBuyButton.Click();
            Log.Info("BTC bought");
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".grid__side.grid__cell > .grid__side-wrap.grid__wrap-in"), "Buy"));
            return this;
        }

        public RoboForexPage CloseAllPositions()
        {
            Positions.Click();
            CloseAllButton.Click();
            Log.Info("All positions closed");
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
            Log.Info("Context menu opened");
            ColorChandgeButton.Click();
            Log.Info("Color Scheme changed");
            Body.Click();
            Log.Info("Context menu closed");
            return this;
        }

        public RoboForexPage ChangeLanguageToRussian()
        {
            LanguageSelector.Click();
            Log.Info("Language selector opened");
            RussianLanguageSelector.Click();
            Log.Info("Language Changed");
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".modal-window__title"), "Авторизация"));
            return this;
        }
    }
}

