using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void ChangeLanguageTest()
        {
            _page.ChangeLanguageToRussian();
            Assert.That(_page.ModalWindowTitle.Text, Is.EqualTo("Авторизация"));
        }

        [Test]
        public void CreateSellStopOrderTest()
        {
            _page.CreateOrder().CreateSellStopOrder();
            Assert.That(_page.PendingOrderType.Text, Is.EqualTo("Sell Stop"));
        }

        [Test]
        public void CreateBuyStopOrderTest()
        {
            _page.CreateOrder().CreateBuyStopOrder();
            Assert.That(_page.PendingOrderType.Text, Is.EqualTo("Buy Stop"));
        }

        [Test]
        public void CreateSellLimitOrderTest()
        {
            _page.CreateOrder().CreateSellLimitOrder();
            Assert.That(_page.PendingOrderType.Text, Is.EqualTo("Sell Limit"));
        }

        [Test]
        public void CreateBuyLimitOrderTest()
        {
            _page.CreateOrder().CreateBuyLimitOrder();
            Assert.That(_page.PendingOrderType.Text, Is.EqualTo("Buy Limit"));
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