using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using NUnit.Framework;

using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Sl.PassiveView.Test
{

    [TestFixture]
    public class TestMainController
    {

/*        private void Click(Button button)
        {
            if (!button.Enabled)
                Assert.Fail("Attempt to click button '" + button.Text + "' while it is not enabled");

            MethodInfo onClick = button.GetType().GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
            onClick.Invoke(button, new object[] { null });
        }*/

        [Test] [Ignore("RGB - WIP")]
        public void Test_WhenViewIsCreated_Then_MessageIsHidden()
        {
            MainView view = new MainView();

            Assert.AreEqual(true, view.ShowMessage.IsEnabled);
            Assert.AreEqual(false, view.Message.Visibility);
            Assert.AreEqual(false, view.SelectColourMessage.Visibility);
            Assert.AreEqual(false, view.ColourSelection.Visibility);
            Assert.AreEqual(false, view.HideMessage.Visibility);
        }

/*        [Test]
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
        public void Test_WhenHideMessageIsClicked_AndUserConfirms_Then_MessageIsHidden()
        {
            MockRepository mocks = new MockRepository();
            DialogHandler dialogHandler = mocks.StrictMock<DialogHandler>();
            MainView view = new TestableView(dialogHandler);

            Expect
                .Call(dialogHandler.ShowMessageBox("Are you sure?", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                .Return(DialogResult.Yes);

            mocks.ReplayAll();
            Click(view.ShowMessage);
            Click(view.HideMessage);
            mocks.VerifyAll();

            Assert.AreEqual(true, view.ShowMessage.Enabled);
            Assert.AreEqual(false, view.IsVisible(view.Message));
            Assert.AreEqual(false, view.IsVisible(view.SelectColourMessage));
            Assert.AreEqual(false, view.IsVisible(view.ColourSelection));
            Assert.AreEqual(false, view.IsVisible(view.HideMessage));
        }

        [Test]
        public void Test_WhenHideMessageIsClicked_AndUserDeclines_Then_MessageRemainsVisible()
        {
            MockRepository mocks = new MockRepository();
            DialogHandler dialogHandler = mocks.StrictMock<DialogHandler>();
            MainView view = new TestableView(dialogHandler);

            Expect
                .Call(dialogHandler.ShowMessageBox( "Are you sure?",
                                                    "Check",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button1))
                .Return(DialogResult.No);

            mocks.ReplayAll();
            Click(view.ShowMessage);
            Click(view.HideMessage);
            mocks.VerifyAll();

            Assert.AreEqual(false, view.ShowMessage.Enabled);
            Assert.AreEqual(true, view.IsVisible(view.Message));
            Assert.AreEqual(true, view.IsVisible(view.SelectColourMessage));
            Assert.AreEqual(true, view.IsVisible(view.ColourSelection));
            Assert.AreEqual(true, view.IsVisible(view.HideMessage));
        }

        [Test]
        public void Test_WhenColourIsSelected_Then_MessageColourChanges()
        {
            MainView view = new TestableView();

            Click(view.ShowMessage);

            view.ColourSelection.SelectedIndex = (int)SelectedColour.Green;
            Assert.AreEqual(Color.Green, view.Message.ForeColor);

            view.ColourSelection.SelectedIndex = (int)SelectedColour.Red;
            Assert.AreEqual(Color.Red, view.Message.ForeColor);

            view.ColourSelection.SelectedIndex = (int)SelectedColour.Black;
            Assert.AreEqual(Color.Black, view.Message.ForeColor);
        }*/

    }

}
