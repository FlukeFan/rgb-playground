
using NUnit.Framework;
using WatiN.Core;
using Microsoft.VisualStudio.WebHost;

namespace WatinExample
{
    [TestFixture]
    public class WatinExample
    {
        [Test]
        public void OpenBrowser()
        {
            IE ie = new IE();

            string path = @"C:\work\rgb\playground\at\watin\site";
            Server server = new Server(8181, "/", path);
            server.Start();

            ie.GoTo("http://localhost:8181");
            System.Threading.Thread.Sleep(5000);

            ie.Dispose();
            server.Stop();
        }
    }
}
