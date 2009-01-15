
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
        private string _waitSignal = null;
        private DateTime _waitTime;

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
            _timer.Duration = new TimeSpan(0, 0, 0, 0, 50);
            _timer.Completed += new EventHandler(timer_Completed);
            _timer.Begin();
        }

        private void WaitUntil(string signal)
        {
            if (_waitSignal != null)
                throw new Exception("Already waiting for " + _waitSignal);

            _waitSignal = signal;
            _waitTime = DateTime.Now;
            Write("Waiting for " + signal);
        }

        private void Arrived(string signal)
        {
            if (_waitSignal != signal)
                throw new Exception("Unexpected arrival of " + signal);

            _waitSignal = null;
            Write("Arrival of " + signal);
        }

        private void timer_Completed(object sender, EventArgs evt)
        {
            if (_waitSignal != null)
            {
                if (DateTime.Now - _waitTime < new TimeSpan(0, 0, 30))
                {
                    _timer.Begin();
                    return;
                }
                else
                {
                    throw new Exception("Timeout waiting for " + _waitSignal);
                }
            }

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
            _client.GetPersonListCompleted += Test2_Response;
            _client.GetPersonList();
            WaitUntil("Test2");
        }

        public void Test2_Response(ServiceCallStatus callStatus)
        {
            Arrived("Test2");
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
            _client.CollatePersonCompleted += Test6_Response;
            _client.CollatePerson(
                new Person() { Age=30, Gender=PersonGender.Male },
                new Person() { Age=40, Gender=PersonGender.Female });
            WaitUntil("Test6");
        }

        public void Test6_Response(ServiceCallStatus callStatus)
        {
            Arrived("Test6");
            Person person = _client.CollatePerson(callStatus);
            Write("Test6 callback person Age (70) = " + person.Age);
        }

        public void Test7()
        {
            _client.ReturnVoidOrThrowCompleted += Test7_Response;
            _client.ReturnVoidOrThrow(0);
            WaitUntil("Test7");
        }

        public void Test7_Response(ServiceCallStatus callStatus)
        {
            Arrived("Test7");
            _client.ReturnVoidOrThrowCompleted -= Test7_Response;
            _client.ReturnVoidOrThrow(callStatus);
            Write("Test7 callback pass");
        }

        public void Test8()
        {
            _client.ReturnVoidOrThrowCompleted += Test8_Response;
            _client.ReturnVoidOrThrow(1);
            WaitUntil("Test8");
        }

        public void Test8_Response(ServiceCallStatus callStatus)
        {
            Arrived("Test8");
            _client.ReturnVoidOrThrowCompleted -= Test8_Response;
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
            _client.ReturnVoidOrThrowCompleted += Test9_Response;
            _client.ReturnVoidOrThrow(2);
            WaitUntil("Test9");
        }

        public void Test9_Response(ServiceCallStatus callStatus)
        {
            Arrived("Test9");
            _client.ReturnVoidOrThrowCompleted -= Test9_Response;
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
            _client.ReturnVoidOrThrowCompleted += Test10_Response;
            _client.ReturnVoidOrThrow(3);
            WaitUntil("Test10");
        }

        public void Test10_Response(ServiceCallStatus callStatus)
        {
            Arrived("Test10");
            _client.ReturnVoidOrThrowCompleted -= Test10_Response;
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