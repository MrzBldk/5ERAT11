using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using _5ERAT11.Models;
using _5ERAT11.Pages;
using _5ERAT11.Services;
using _5ERAT11.Driver;
using _5ERAT11.Utils;

namespace _5ERAT11
{
    class Tests
    {
        private IWebDriver _driver;
        private RoboForexPage _page;

        [SetUp]
        public void InitBrowserAndLogIn()
        {
            _driver = DriverInstance.GetInstance();
            User testUser = UserCreator.WithCredentialsFromProperty();
            _page = new RoboForexPage(_driver).
                OpenPage().
                AllowCookies().
                Login(testUser.Email, testUser.Password);
        }

        [Test]
        [TestCase("1")]
        public void BTCFastSellTest(string amount)
        {
            _page.SellBTC(amount);
            string expectedOrderSize = amount.ConvertOrderSizeToRoboForexFormat();

            Assert.Multiple(() =>
            {
                Assert.That(_page.OrderSize.Text, Is.EqualTo(expectedOrderSize));
                Assert.That(_page.OrderType.Text, Is.EqualTo("Sell"));
                Assert.That(_page.OrderSymbol.Text, Is.EqualTo("BTCUSD"));
            });
        }

        [Test]
        [TestCase("1")]
        public void BTCFastBuyTest(string amount)
        {
            _page.BuyBTC(amount);
            string expectedOrderSize = amount.ConvertOrderSizeToRoboForexFormat();

            Assert.Multiple(() =>
            {
                Assert.That(_page.OrderSize.Text, Is.EqualTo(expectedOrderSize));
                Assert.That(_page.OrderType.Text, Is.EqualTo("Buy"));
                Assert.That(_page.OrderSymbol.Text, Is.EqualTo("BTCUSD"));
            });
        }

        [Test]
        public void CloseAllPositionsTests()
        {
            _page.CloseAllPositions();
            string positionCounterDisplay = _page.PositionCounter.GetCssValue("display");
            Assert.That(positionCounterDisplay, Is.EqualTo("none"));
        }

        [Test]
        public void ChangeColorSchemeTest()
        {
            string OldColorScheme = _page.Body.GetCssValue("Color");
            _page.ChangeColorScheme();
            string NewColorScheme = _page.Body.GetCssValue("Color");
            Assert.That(OldColorScheme, Is.Not.EqualTo(NewColorScheme));
        }

        [Test]
        public void ChangeLanguageToRussianTest()
        {
            _page.ChangeLanguageToRussian();
            Assert.That(_page.ModalWindowTitle.Text, Is.EqualTo("Авторизация"));
        }

        [Test]
        [TestCase("1")]
        public void CreateBTCSellStopOrderTest(string price)
        {
            _page.CreateOrderModalWindow.OpenOrderCreatingWindow().CreateSellStopOrder(price);
            string expectedOrderPrice = price.ConvertOrderSizeToRoboForexFormat();
            Assert.Multiple(() =>
            {
                Assert.That(_page.CreateOrderModalWindow.PendingOrderType.Text, Is.EqualTo("Sell Stop"));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderPrice.Text, Is.EqualTo(expectedOrderPrice));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderSymbol.Text, Is.EqualTo("BTCUSD"));
            });
        }

        [Test]
        [TestCase("9999999")]
        public void CreateBTCBuyStopOrderTest(string price)
        {
            _page.CreateOrderModalWindow.OpenOrderCreatingWindow().CreateBuyStopOrder(price);
            string expectedOrderPrice = price.ConvertOrderSizeToRoboForexFormat();
            Assert.Multiple(() =>
            {
                Assert.That(_page.CreateOrderModalWindow.PendingOrderType.Text, Is.EqualTo("Buy Stop"));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderPrice.Text, Is.EqualTo(expectedOrderPrice));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderSymbol.Text, Is.EqualTo("BTCUSD"));
            });
        }

        [Test]
        [TestCase("9999999")]
        public void CreateBTCSellLimitOrderTest(string price)
        {
            _page.CreateOrderModalWindow.OpenOrderCreatingWindow().CreateSellLimitOrder(price);
            string expectedOrderPrice = price.ConvertOrderSizeToRoboForexFormat();
            Assert.Multiple(() =>
            {
                Assert.That(_page.CreateOrderModalWindow.PendingOrderType.Text, Is.EqualTo("Sell Limit"));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderPrice.Text, Is.EqualTo(expectedOrderPrice));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderSymbol.Text, Is.EqualTo("BTCUSD"));
            });
        }

        [Test]
        [TestCase("1")]
        public void CreateBTCBuyLimitOrderTest(string price)
        {
            _page.CreateOrderModalWindow.OpenOrderCreatingWindow().CreateBuyLimitOrder(price);
            string expectedOrderPrice = price.ConvertOrderSizeToRoboForexFormat();
            Assert.Multiple(() =>
            {
                Assert.That(_page.CreateOrderModalWindow.PendingOrderType.Text, Is.EqualTo("Buy Limit"));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderPrice.Text, Is.EqualTo(expectedOrderPrice));
                Assert.That(_page.CreateOrderModalWindow.PendingOrderSymbol.Text, Is.EqualTo("BTCUSD"));
            });
        }

        [TearDown]
        public void CloseBrowser()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                ScreenShot.MakeScreenShot();
            }
            Log.Info($"{TestContext.CurrentContext.Test.Name} {TestContext.CurrentContext.Result.Outcome} { TestContext.CurrentContext.Result.Message}");
            DriverInstance.CloseBrowser();
        }
    }
}