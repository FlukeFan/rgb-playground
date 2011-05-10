
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfTest
{

    [Serializable]
    public class MyClass
    {

        private string _name;
        private MyClass _other;

        public MyClass(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public MyClass Other
        {
            get { return _other; }
            set { _other = value; }
        }
    }

    [Serializable]
    public class Parent
    {
        protected string _name;

        public string Name
        {
            get { return _name; }
        }

    }


    [ServiceContract()]
    public interface IWcfTest
    {

        [OperationContract]
        MyClass GetCopy(MyClass otherClass);

        [OperationContract]
        IList<Parent> GetParentList();

        [OperationContract]
        Parent GetParent();

    }

    public class Service : IWcfTest
    {

        public MyClass GetCopy(MyClass otherClass)
        {
            MyClass copy = new MyClass("copy of " + otherClass.Name);
            MyClass copy2 = new MyClass("copy of other " + otherClass.Other.Name);
            copy.Other = copy2;
            //copy2.Other = copy;
            return copy;
        }

        public IList<Parent> GetParentList()
        {
            return null;
        }

        public Parent GetParent()
        {
            return null;
        }

    }


}

