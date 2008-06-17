
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

using Demo.Domain;

namespace Demo.Domain
{
    [DataContract(Name = "Composite1", Namespace = "http://schemas.datacontract.org/2004/07/Demo.Domain")]
    public class Composite1
    {
        private string _value;
        private Composite2 _c2;

        [DataMember(Name="_value")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Composite2 C2
        {
            get { return _c2; }
            set { _c2 = value; }
        }
    }

    public class Composite2
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}

namespace Sl2
{

    [ServiceContract]
    public interface ITestService
    {
        [OperationContract(AsyncPattern=true, Action = "http://tempuri.org/ITestService/GetC1", ReplyAction = "http://tempuri.org/ITestService/GetC1Response")]
        System.IAsyncResult BeginGetC1(System.AsyncCallback callback, object asyncState);

        Composite1 EndGetC1(System.IAsyncResult result);
    }

    public partial class GetC1CompletedEventArgs : AsyncCompletedEventArgs
    {

        private object[] results;

        public GetC1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public Composite1 Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return ((Composite1)(this.results[0]));
            }
        }
    }

    public partial class ServiceClient : System.ServiceModel.ClientBase<ITestService>, ITestService
    {

        private BeginOperationDelegate onBeginGetC1Delegate;

        private EndOperationDelegate onEndGetC1Delegate;

        private SendOrPostCallback onGetC1CompletedDelegate;

        private static Binding defaultBinding = new System.ServiceModel.BasicHttpBinding();

        private static EndpointAddress defaultAddress = new System.ServiceModel.EndpointAddress("http://localhost/samples/TestService.svc");

        public ServiceClient()
            :
                this(defaultBinding, defaultAddress)
        {
        }

        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
            :
                base(binding, remoteAddress)
        {
        }

        public event EventHandler<GetC1CompletedEventArgs> GetC1Completed;

        IAsyncResult ITestService.BeginGetC1(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetC1(callback, asyncState);
        }

        Composite1 ITestService.EndGetC1(System.IAsyncResult result)
        {
            return base.Channel.EndGetC1(result);
        }

        private IAsyncResult OnBeginGetC1(object[] inValues, System.AsyncCallback callback, object asyncState)
        {
            return ((ITestService)(this)).BeginGetC1(callback, asyncState);
        }

        private object[] OnEndGetC1(System.IAsyncResult result)
        {
            Composite1 retVal = ((ITestService)(this)).EndGetC1(result);
            return new object[] {
                    retVal};
        }

        private void OnGetC1Completed(object state)
        {
            if ((this.GetC1Completed != null))
            {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetC1Completed(this, new GetC1CompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }

        public void GetC1Async()
        {
            this.GetC1Async(null);
        }

        public void GetC1Async(object userState)
        {
            if ((this.onBeginGetC1Delegate == null))
            {
                this.onBeginGetC1Delegate = new BeginOperationDelegate(this.OnBeginGetC1);
            }
            if ((this.onEndGetC1Delegate == null))
            {
                this.onEndGetC1Delegate = new EndOperationDelegate(this.OnEndGetC1);
            }
            if ((this.onGetC1CompletedDelegate == null))
            {
                this.onGetC1CompletedDelegate = new SendOrPostCallback(this.OnGetC1Completed);
            }
            base.InvokeAsync(this.onBeginGetC1Delegate, new object[] {}, this.onEndGetC1Delegate, this.onGetC1CompletedDelegate, userState);
        }

        protected override ITestService CreateChannel()
        {
            return new ServiceClientChannel(this);
        }

        private class ServiceClientChannel : ChannelBase<ITestService>, ITestService
        {

            public ServiceClientChannel(ClientBase<ITestService> client)
                :
                    base(client)
            {
            }

            public IAsyncResult BeginGetC1(AsyncCallback callback, object asyncState)
            {
                object[] _args = new object[0];
                IAsyncResult _result = base.BeginInvoke("GetC1", _args, callback, asyncState);
                return _result;
            }

            public Composite1 EndGetC1(IAsyncResult result)
            {
                object[] _args = new object[0];
                Composite1 _result = ((Composite1)(base.EndInvoke("GetC1", _args, result)));
                return _result;
            }

        }
    }

}