
using System;
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

    [ServiceContract()]
    public interface IWcfTest
    {

        [OperationContract]
        MyClass GetCopy(MyClass otherClass);

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

    }


}

