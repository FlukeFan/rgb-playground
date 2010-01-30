
using NUnit.Framework;
using WatiN.Core;

namespace WatinExample
{
    [TestFixture]
    public class WatinExample
    {
        [Test]
        public void OpenBrowser()
        {
            IE ie = new IE();

            ie.Dispose();
        }
    }
}
