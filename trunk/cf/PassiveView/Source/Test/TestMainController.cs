using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using NUnit.Framework;

namespace Cf.PassiveView.Source.Test
{

    [TestFixture]
    public class TestMainController
    {

        private void Click(Button button)
        {
            if (!button.Enabled)
                Assert.Fail("Attempt to click button '" + button.Text + "' while it is not enabled");

            MethodInfo onClick = button.GetType().GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
            onClick.Invoke(button, new object[] { null });
        }

        [Test]
        public void Test_WhenViewIsCreate_Then_MessageIsHidden()
        {
            MainView view = new TestableView();

            Assert.AreEqual(true, view.ShowMessage.Enabled);
            Assert.AreEqual(false, view.IsVisible(view.Message));
            Assert.AreEqual(false, view.IsVisible(view.SelectColourMessage));
            Assert.AreEqual(false, view.IsVisible(view.ColourSelection));
            Assert.AreEqual(false, view.IsVisible(view.HideMessage));
        }

        [Test]
        public void Test_WhenShowMessageIsClicked_Then_MessageIsDisplayed_And_ColourSelectionIsPopulated()
        {
            MainView view = new TestableView();

            Click(view.ShowMessage);

            Assert.AreEqual(false, view.ShowMessage.Enabled);
            Assert.AreEqual(true, view.IsVisible(view.Message));
            Assert.AreEqual(true, view.IsVisible(view.SelectColourMessage));
            Assert.AreEqual(true, view.IsVisible(view.ColourSelection));
            Assert.AreEqual(true, view.IsVisible(view.HideMessage));

            Assert.AreEqual(3, view.ColourSelection.Items.Count);
            Assert.AreEqual(0, view.ColourSelection.SelectedIndex);
        }

        [Test]
        public void Test_WhenHideMessageIsClicked_Then_MessageIsHidden()
        {
            MainView view = new TestableView();

            Click(view.ShowMessage);
            Click(view.HideMessage);

            Assert.AreEqual(true, view.ShowMessage.Enabled);
            Assert.AreEqual(false, view.IsVisible(view.Message));
            Assert.AreEqual(false, view.IsVisible(view.SelectColourMessage));
            Assert.AreEqual(false, view.IsVisible(view.ColourSelection));
            Assert.AreEqual(false, view.IsVisible(view.HideMessage));
        }

    }

}
