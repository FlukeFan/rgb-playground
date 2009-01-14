
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using SlWcf.Domain;
using SlWcf.Services;

namespace SlWcf
{
    public class Page : UserControl
    {

        private TestServiceClient _client;

        private Button _send;
        private ScrollViewer _scrollViewer;
        private StackPanel _messages;
        private int _callIndex = 0;
        private List<MethodInfo> _calls = new List<MethodInfo>();
        private Storyboard _timer;

        public Page()
        {
            Loaded += new RoutedEventHandler(Page_Loaded);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EndpointAddress endpointAddress = new EndpointAddress("http://localhost/SlWcfExample/TestService.svc");
            _client = new TestServiceClient(endpointAddress, Dispatcher);

            _send = (Button)FindName("Send");
            _send.Click += new RoutedEventHandler(Send_Click);

            _scrollViewer = (ScrollViewer)FindName("ScrollViewer");
            _messages = (StackPanel)FindName("Messages");

            foreach (MethodInfo testMethod in GetType().GetMethods())
            {
                if (testMethod.GetParameters().Length == 0
                    && testMethod.DeclaringType == GetType())
                {
                    _calls.Add(testMethod);
                }
            }

            _timer = new Storyboard();
            _timer.Duration = new TimeSpan(0, 0, 0, 0, 500);
            _timer.Completed += new EventHandler(timer_Completed);
            _timer.Begin();
        }

        private void timer_Completed(object sender, EventArgs evt)
        {
            if (_callIndex < _calls.Count)
            {
                MethodInfo test = _calls[_callIndex];
                Write("Calling " + test.Name);
                try
                {
                    test.Invoke(this, null);
                    Write("Pass");
                }
                catch (Exception e)
                {
                    Write("Fail");
                    Write(e.ToString());
                }
                _callIndex++;
                _timer.Begin();
            }
        }

        public void Test1()
        {
            Send_Click(null, null);
        }

        public void Test2()
        {
            Write("Calling ...");
            _client.GetPersonList(Test2_Response);
            Write("Called");
        }

        public void Test2_Response(ServiceCallStatus callStatus)
        {
            IList<Person> personList = _client.GetPersonList(callStatus);
            Write("Name1 (Her)=" + personList[0].Name);
            Write("Name2 (Him)=" + personList[1].Name);
        }

        public void Test3()
        {
            Write("Test3 obsolete");
        }

        public void Test4()
        {
            Write("Test4 Obsolete");
        }

        public void Test5()
        {
            Write("Test5 Obsolete");
        }

        public void Test6()
        {
            _client.CollatePerson(
                new Person() { Age=30, Gender=PersonGender.Male },
                new Person() { Age=40, Gender=PersonGender.Female },
                Test6_Response);
        }

        public void Test6_Response(ServiceCallStatus callStatus)
        {
            Person person = _client.CollatePerson(callStatus);
            Write("Test6 callback person Age (70) = " + person.Age);
        }

        public void Test7()
        {
            _client.ReturnVoidOrThrow(0, Test7_Response);
        }

        public void Test7_Response(ServiceCallStatus callStatus)
        {
            _client.ReturnVoidOrThrow(callStatus);
            Write("Test7 callback pass");
        }

        public void Test8()
        {
            _client.ReturnVoidOrThrow(1, Test8_Response);
        }

        public void Test8_Response(ServiceCallStatus callStatus)
        {
            try
            {
                _client.ReturnVoidOrThrow(callStatus);
                throw new Exception("exception not thrown!");
            }
            catch (NameNotUniqueException e)
            {
                Write(e.Message);
                Write("e.DuplicateName = " + e.DuplicateName);
            }
        }

        public void Test9()
        {
            _client.ReturnVoidOrThrow(2, Test9_Response);
        }

        public void Test9_Response(ServiceCallStatus callStatus)
        {
            try
            {
                _client.ReturnVoidOrThrow(callStatus);
                throw new Exception("exception not thrown!");
            }
            catch (ArgumentException e)
            {
                Write(e.GetType().ToString());
                Write(e.Message);
            }
        }

        public void Test10()
        {
            _client.ReturnVoidOrThrow(3, Test10_Response);
        }

        public void Test10_Response(ServiceCallStatus callStatus)
        {
            try
            {
                _client.ReturnVoidOrThrow(callStatus);
                throw new Exception("exception not thrown!");
            }
            catch (Exception e)
            {
                Write(e.Message);
            }
        }

        private void Write(string message)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = DateTime.Now + " - " + message;
            _messages.Children.Add(textBlock);
            _scrollViewer.ScrollToVerticalOffset(_scrollViewer.ExtentHeight);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Write("Clicked");
        }

    }
}