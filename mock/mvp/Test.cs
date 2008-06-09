
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Test
{

    public interface IService
    {
        IList<string> GetList(int i);
    }

    public interface IButton
    {
        event EventHandler Click;
        string Text { get; }
    }

    public interface ITextBlock
    {
        string Text { set; }
    }

    public interface IView
    {
        IButton Button { get; set; }
        ITextBlock TextBlock { get; set; }
    }

    public class Presenter
    {
        private IView _view;

        public Presenter(   IView       view,
                            IService    service)
        {
            _view = view;
            _view.Button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
        }

    }

    [TestFixture]
    public class TestFixture
    {

        [Test]
        public void Test1()
        {
            MockRepository mocks = new MockRepository();
            IView viewStub = mocks.Stub<IView>();
            viewStub.Button = mocks.CreateMock<IButton>();
            viewStub.TextBlock = mocks.CreateMock<ITextBlock>();

            SetupResult.For(viewStub.Button.Text).Return("my prefix");

            viewStub.Button.Click += null;
            LastCall.IgnoreArguments();
            IEventRaiser buttonClickEvent = LastCall.GetEventRaiser();

            IService service = mocks.CreateMock<IService>();
            SetupResult.For(service.GetList(0)).Return(new List<string>(new string[] { "item 1", "item " }));

            // add expectation for list values

            mocks.ReplayAll();
            Presenter presenter = new Presenter(viewStub, service);
            buttonClickEvent.Raise(null, null);
            mocks.VerifyAll();
        }

    }

}

