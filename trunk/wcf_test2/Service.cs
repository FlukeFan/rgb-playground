
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfTest
{

    [Serializable]
    public class Parent
    {

        private string _name;
        private IList<Child> _children = new List<Child>();


        public Parent(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public IList<Child> Children
        {
            get { return _children; }
        }

    }

    [Serializable]
    public class Child
    {
        private string _name;
        private Parent _parent;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Parent Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

    }

    [Serializable]
    public class MyException : ApplicationException
    {

        private string _info;

        public MyException(string info)
        {
            _info = info;
        }

        /// <summary>
        /// Constructor for serialization
        /// </summary>
        protected MyException(  SerializationInfo   info,
                                StreamingContext    context) : base (info, context)
        {
            _info = (string) info.GetString("_info");
        }

        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }

        /// <summary>
        /// Overrides the ISerializable implementation
        /// </summary>
        public override void GetObjectData (SerializationInfo   info,
                                            StreamingContext    context)
        {
            // call the base exception class to ensure proper serialization
            base.GetObjectData (info, context);

            // add the custom properties into the serialization stream
            info.AddValue("_info", _info);
        }

    }


    [ServiceContract()]
    public interface IWcfTest
    {

        [OperationContract]
        [FaultContract(typeof(MyException))]
        Parent GetGraph(Parent parentCopy);

    }

    public class MyService : IWcfTest
    {

        public Parent GetGraph(Parent parentCopy)
        {
            if ((parentCopy != null) && (parentCopy.Name == "error"))
            {
                throw new FaultException<MyException>(new MyException("error info from service"));
            }

            Parent parent = new Parent("parent from " + Process.GetCurrentProcess().Id.ToString());

            Child child1 = new Child(); child1.Name="child 1";
            Child child2 = new Child(); child2.Name="child 2";

            parent.Children.Add(child1);
            //child1.Parent = parent; -- can't do this yet
            parent.Children.Add(child2);

            return parent;
        }

    }

    public class ServiceRunner
    {

      private static ServiceHost _serviceHost;

      public static void Main(string[] args)
      {
        StartWCFServer();
        Console.ReadLine();
      }

      private static void StartWCFServer()
      {
        _serviceHost = new ServiceHost(typeof(MyService));
        _serviceHost.Open();
        Console.WriteLine("WCF Server is running ... " + Process.GetCurrentProcess().Id.ToString());
      }

    }


}

