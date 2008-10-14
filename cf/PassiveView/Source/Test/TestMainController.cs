using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace Cf.PassiveView.Source.Test
{

    [TestFixture]
    public class TestMainController
    {

        [Test]
        public void Test_WhenViewIsCreate_Then_MessageIsHidden_And_ColourSelectionIsPopulated()
        {
            MainView view = new TestableView();

            Assert.AreEqual(false, view.IsVisible(view.Message));
            Assert.AreEqual(false, view.IsVisible(view.ColourSelection));
            Assert.AreEqual(false, view.IsVisible(view.HideMessage));
        }

    }

}
