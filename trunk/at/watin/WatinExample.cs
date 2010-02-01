using System;
using System.Linq;
using NUnit.Framework;
using WatiN.Core;
using Microsoft.VisualStudio.WebHost;

namespace WatinExample
{
    [TestFixture]
    public class WatinExample
    {
        private Server _server;
        private IE _ie;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            string path = @"C:\work\rgb\playground\at\watin\site";
            _server = new Server(8181, "/", path);
            _server.Start();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _server.Stop();
        }

        [SetUp]
        public void SetUp()
        {
            _ie = new IE(true);
        }

        [TearDown]
        public void TearDown()
        {
            _ie.Dispose();
        }

        protected void AssertCanSeeText(string text)
        {
            Console.WriteLine("Verify can see the text: " + text);
            Assert.IsTrue(_ie.Body.OuterText.Contains(text), "Could not see text: " + text);
        }

        protected void OpenHomePage()
        {
            Console.WriteLine("Open the home page");
            _ie.GoTo("http://localhost:8181");
        }

        protected void ClickButton(string buttonText)
        {
            Console.WriteLine("Click button: " + buttonText);
            Button button = _ie.Buttons.Where(b => b.Value == buttonText).First();
            button.Click();
        }

        [Test]
        public void can_open_the_home_page()
        {
            OpenHomePage();

            AssertCanSeeText("Hello");
        }

        [Test]
        public void can_click_one()
        {
            OpenHomePage();
            ClickButton("Click1");

            AssertCanSeeText("Clicked");
        }
    }
}
