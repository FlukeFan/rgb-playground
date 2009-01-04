
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows.Threading;

using Demo.Domain;
using Demo.Services;

namespace Demo.Domain
{

    public enum PersonGender
    {
        Male = 1,
        Female = 2,
    }

    [DataContract]
    public class Person
    {
        [System.Runtime.Serialization.DataMemberAttribute(Name="_name", IsRequired=true)]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Age>k__BackingField", IsRequired=true)]
        public int Age;
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Gender>k__BackingField", IsRequired=true)]
        public PersonGender Gender { get; set; }
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Father>k__BackingField", IsRequired=true)]
        public Person Father { get; set; }
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Children>k__BackingField", IsRequired=true)]
        public IList<Person> Children { get; set; }
    }

    [System.Runtime.Serialization.DataContractAttribute(Name="DomainExceptionFault", Namespace="http://schemas.datacontract.org/2004/07/Demo.Domain")]
    public class DomainExceptionFault
    {

    }

    [DataContract]
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

        [DataMember(Name = "_c2")]
        public Composite2 C2
        {
            get { return _c2; }
            set { _c2 = value; }
        }
    }

    [DataContract]
    public class Composite2
    {
        private int _value;

        [DataMember(Name = "_value")]
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

}


namespace Demo.Services
{

    public delegate void ServiceCallback(ServiceCallStatus status);

    public class ServiceCallStatus
    {
        public ServiceCallback Callback { get; set; }
        public IAsyncResult AsyncResult { get; set; }
    }

    [DataContract]
    public class ServiceResult
    {
        [DataMember] public bool IsVoid { get; set; }
        [DataMember] public bool IsError { get; set; }
        [DataMember] public string ExceptionMessage { get; set; }
        [DataMember] public string ExceptionClass { get; set; }
        [DataMember] public IDictionary<string, object> Properties { get; set; }
    }

    [DataContract]
    public class ServiceResult<T> : ServiceResult
    {
        [DataMember]
        public T Result { get; set; }
    }

}

namespace SlWcf
{

    public class MinimalClient : ClientBase<ITestServiceDp>
    {

        private Dispatcher _dispatcher;

        public MinimalClient(Dispatcher dispatcher) : base(new BasicHttpBinding(), new EndpointAddress("http://localhost/sl_wcf/TestService.svc"))
        {
            _dispatcher = dispatcher;
        }

        public void Invoke(AsyncCallback callback, IAsyncResult result)
        {
            if (_dispatcher == null)
            {
                callback(result);
            }
            else
            {
                _dispatcher.BeginInvoke(callback, new object[] { result });
            }
        }

        private void InvokeResultOnUiThread(ServiceCallStatus serviceCallStatus)
        {
            if (_dispatcher == null)
            {
                serviceCallStatus.Callback(serviceCallStatus);
            }
            else
            {
                _dispatcher.BeginInvoke(serviceCallStatus.Callback, new object[] { serviceCallStatus });
            }
        }

        public IAsyncResult GetPersonGraphAsync(AsyncCallback asyncCallback)
        {
            return Channel.BeginGetPersonGraph(GetPersonGraphResponse, asyncCallback);
        }

        private void GetPersonGraphResponse(IAsyncResult result)
        {
            AsyncCallback callback = (AsyncCallback)result.AsyncState;
            Invoke(callback, result);
        }

        public IAsyncResult CollatePerson(Person person1, Person person2, ServiceCallback serviceCallback)
        {
            return Channel.BeginCollatePerson(person1, person2, CollatePersonResponse, new ServiceCallStatus() { Callback = serviceCallback });
        }

        private void CollatePersonResponse(IAsyncResult result)
        {
            ServiceCallStatus serviceCallStatus = (ServiceCallStatus)result.AsyncState;
            serviceCallStatus.AsyncResult = result;
            InvokeResultOnUiThread(serviceCallStatus);
        }

        public Person CollatePerson(ServiceCallStatus serviceCallStatus)
        {
            ServiceResult<Person> serviceResult = Channel.EndCollatePerson(serviceCallStatus.AsyncResult);
            return serviceResult.Result;
        }

    }

    [ServiceContract(Name="ITestService")]
    public interface ITestServiceDp
    {
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetPersonGraph(AsyncCallback callback, object state);
        Person EndGetPersonGraph(IAsyncResult result);

        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginCollatePerson(Person person1, Person person2, AsyncCallback callback, object state);
        ServiceResult<Person> EndCollatePerson(IAsyncResult result);
    }

    [ServiceContract]
    public interface ITestService
    {
        [OperationContract(AsyncPattern=true, Action = "http://tempuri.org/ITestService/GetC1", ReplyAction = "http://tempuri.org/ITestService/GetC1Response")]
        System.IAsyncResult BeginGetC1(System.AsyncCallback callback, object asyncState);

        Composite1 EndGetC1(System.IAsyncResult result);

        [OperationContract(AsyncPattern=true, Action = "http://tempuri.org/ITestService/GetPersonGraph", ReplyAction = "http://tempuri.org/ITestService/GetPersonGraphResponse")]
        System.IAsyncResult BeginGetPersonGraph(System.AsyncCallback callback, object asyncState);

        Person EndGetPersonGraph(System.IAsyncResult result);

        [OperationContract(AsyncPattern=true, Action = "http://tempuri.org/ITestService/GetPersonThrowError", ReplyAction = "http://tempuri.org/ITestService/GetPersonThrowErrorResponse")]
        System.IAsyncResult BeginGetPersonThrowError(System.AsyncCallback callback, object asyncState);

        Person EndGetPersonThrowError(System.IAsyncResult result);
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

    public class GetPersonGraphCompletedEventArgs : AsyncCompletedEventArgs
    {

        private object[] results;

        public GetPersonGraphCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public Person Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return ((Person)(this.results[0]));
            }
        }
    }

    public class GetPersonThrowErrorCompletedEventArgs : AsyncCompletedEventArgs
    {

        private object[] results;

        public GetPersonThrowErrorCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public Person Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return ((Person)(this.results[0]));
            }
        }
    }

    public partial class ServiceClient : System.ServiceModel.ClientBase<ITestService>, ITestService
    {

        private BeginOperationDelegate onBeginGetC1Delegate;

        private EndOperationDelegate onEndGetC1Delegate;

        private SendOrPostCallback onGetC1CompletedDelegate;

        private static Binding defaultBinding = new System.ServiceModel.BasicHttpBinding();

        private static EndpointAddress defaultAddress = new System.ServiceModel.EndpointAddress("http://localhost/sl_wcf/TestService.svc");

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

//********************************************************************************************

        private BeginOperationDelegate onBeginGetPersonGraphDelegate;
        private EndOperationDelegate onEndGetPersonGraphDelegate;
        private SendOrPostCallback onGetPersonGraphCompletedDelegate;
        public event EventHandler<GetPersonGraphCompletedEventArgs> GetPersonGraphCompleted;

        IAsyncResult ITestService.BeginGetPersonGraph(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPersonGraph(callback, asyncState);
        }

        Person ITestService.EndGetPersonGraph(System.IAsyncResult result)
        {
            return base.Channel.EndGetPersonGraph(result);
        }

        private IAsyncResult OnBeginGetPersonGraph(object[] inValues, System.AsyncCallback callback, object asyncState)
        {
            return ((ITestService)(this)).BeginGetPersonGraph(callback, asyncState);
        }

        private object[] OnEndGetPersonGraph(System.IAsyncResult result)
        {
            Person retVal = ((ITestService)(this)).EndGetPersonGraph(result);
            return new object[] {
                    retVal};
        }

        private void OnGetPersonGraphCompleted(object state)
        {
            if ((this.GetPersonGraphCompleted != null))
            {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetPersonGraphCompleted(this, new GetPersonGraphCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }

        public void GetPersonGraphAsync()
        {
            this.GetPersonGraphAsync(null);
        }

        public void GetPersonGraphAsync(object userState)
        {
            if ((this.onBeginGetPersonGraphDelegate == null))
            {
                this.onBeginGetPersonGraphDelegate = new BeginOperationDelegate(this.OnBeginGetPersonGraph);
            }
            if ((this.onEndGetPersonGraphDelegate == null))
            {
                this.onEndGetPersonGraphDelegate = new EndOperationDelegate(this.OnEndGetPersonGraph);
            }
            if ((this.onGetPersonGraphCompletedDelegate == null))
            {
                this.onGetPersonGraphCompletedDelegate = new SendOrPostCallback(this.OnGetPersonGraphCompleted);
            }
            base.InvokeAsync(this.onBeginGetPersonGraphDelegate, new object[] {}, this.onEndGetPersonGraphDelegate, this.onGetPersonGraphCompletedDelegate, userState);
        }

//********************************************************************************************

        private BeginOperationDelegate onBeginGetPersonThrowErrorDelegate;
        private EndOperationDelegate onEndGetPersonThrowErrorDelegate;
        private SendOrPostCallback onGetPersonThrowErrorCompletedDelegate;
        public event EventHandler<GetPersonThrowErrorCompletedEventArgs> GetPersonThrowErrorCompleted;

        IAsyncResult ITestService.BeginGetPersonThrowError(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPersonThrowError(callback, asyncState);
        }

        Person ITestService.EndGetPersonThrowError(System.IAsyncResult result)
        {
            return base.Channel.EndGetPersonThrowError(result);
        }

        private IAsyncResult OnBeginGetPersonThrowError(object[] inValues, System.AsyncCallback callback, object asyncState)
        {
            return ((ITestService)(this)).BeginGetPersonThrowError(callback, asyncState);
        }

        private object[] OnEndGetPersonThrowError(System.IAsyncResult result)
        {
            Person retVal = ((ITestService)(this)).EndGetPersonThrowError(result);
            return new object[] {
                    retVal};
        }

        private void OnGetPersonThrowErrorCompleted(object state)
        {
            if ((this.GetPersonThrowErrorCompleted != null))
            {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetPersonThrowErrorCompleted(this, new GetPersonThrowErrorCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }

        public void GetPersonThrowErrorAsync()
        {
            this.GetPersonThrowErrorAsync(null);
        }

        public void GetPersonThrowErrorAsync(object userState)
        {
            if ((this.onBeginGetPersonThrowErrorDelegate == null))
            {
                this.onBeginGetPersonThrowErrorDelegate = new BeginOperationDelegate(this.OnBeginGetPersonThrowError);
            }
            if ((this.onEndGetPersonThrowErrorDelegate == null))
            {
                this.onEndGetPersonThrowErrorDelegate = new EndOperationDelegate(this.OnEndGetPersonThrowError);
            }
            if ((this.onGetPersonThrowErrorCompletedDelegate == null))
            {
                this.onGetPersonThrowErrorCompletedDelegate = new SendOrPostCallback(this.OnGetPersonThrowErrorCompleted);
            }
            base.InvokeAsync(this.onBeginGetPersonThrowErrorDelegate, new object[] {}, this.onEndGetPersonThrowErrorDelegate, this.onGetPersonThrowErrorCompletedDelegate, userState);
        }

//********************************************************************************************

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

            public IAsyncResult BeginGetPersonGraph(AsyncCallback callback, object asyncState)
            {
                object[] _args = new object[0];
                IAsyncResult _result = base.BeginInvoke("GetPersonGraph", _args, callback, asyncState);
                return _result;
            }

            public Person EndGetPersonGraph(IAsyncResult result)
            {
                object[] _args = new object[0];
                Person _result = ((Person)(base.EndInvoke("GetPersonGraph", _args, result)));
                return _result;
            }

            public IAsyncResult BeginGetPersonThrowError(AsyncCallback callback, object asyncState)
            {
                object[] _args = new object[0];
                IAsyncResult _result = base.BeginInvoke("GetPersonThrowError", _args, callback, asyncState);
                return _result;
            }

            public Person EndGetPersonThrowError(IAsyncResult result)
            {
                object[] _args = new object[0];
                Person _result = ((Person)(base.EndInvoke("GetPersonThrowError", _args, result)));
                return _result;
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