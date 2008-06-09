
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml;
using DemoClickOnce.Pages;

namespace DemoClickOnce
{

    public class App : Application
    {

        // Entry point method
        [STAThread]
        public static void Main()
        {
            try
            {
                App app = new App();
                app.Run();
            }
            catch(Exception e)
            {
                MessageBox.Show("error: " + e.ToString());
            }
        }

        public App()
        {
            Startup += Application_Startup;
            DispatcherUnhandledException += Application_UnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new Window();
            MainWindow.Title = "Atlanta Library Project";
            MainWindow.Show();

            MyPage myPage2 = new MyPage();
            //Uri uri = new Uri(@"/DemoClickOnce;;;component/MyPage.xaml", UriKind.Relative);
            //MessageBox.Show(typeof(MyPage).Assembly.GetManifestResourceInfo("MyPage.xaml").FileName);
            //LoadComponent(myPage, uri);
            //MessageBox.Show(XamlWriter.Save(myPage2));

/*string t =
@"<MyPage
                xmlns='clr-namespace:DemoClickOnce.Pages;assembly=DemoClickOnce'
                xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                Name='MyPage'
                Width='600'
                Height='300'>
    <x:StackPanel Name='LayoutRoot' Background='White'>
        <x:Button Content='ClickMe' Width='200' Name='SendButton' />
        <x:TextBlock Name='textResponse' Text='Hello world ...' />
    </x:StackPanel>
</MyPage>";*/

            //MyPage myPage = (MyPage)XamlReader.Load(XmlReader.Create(new StringReader(t)));
            MyPage myPage = (MyPage) XamlReader.Load(typeof(MyPage).Assembly.GetManifestResourceStream("MyPage.xaml"));
            MainWindow.Content = myPage;
            //Stream stream = new FileStream("DemoClickOnce/Pages/MyPage.xaml", FileMode.Open);
            //object t = XamlReader.Load(stream);
            //MyPage myPage = new MyPage();
            //Uri uri = new Uri(@"DemoClickOnce/Pages/MyPage.xaml", UriKind.Relative);
            //LoadComponent(myPage, uri);
        }

        private void Application_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }
    }
}