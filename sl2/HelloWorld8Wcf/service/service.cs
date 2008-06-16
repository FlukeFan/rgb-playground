
using System;
using System.ServiceModel;

namespace Demo.Services
{

    [Serializable]
    public class Composite1
    {
        private string _value;
        private Composite2 _c2;

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

    [Serializable]
    public class Composite2
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    [Serializable]
    public class CustomException : ApplicationException
    {
        private Composite1 _c1;

        public CustomException(Composite1 c1)
        {
            _c1 = c1;
        }

        public Composite1 C1
        {
            get { return _c1; }
        }
    }

    [ServiceContract()]
    interface ITestService
    {
        [OperationContract()]
        Composite1 GetC1();

        [OperationContract()]
        void Throw();
    }

    public class TestService : ITestService
    {
        public Composite1 GetC1()
        {
            Composite1 c = new Composite1();
            c.Value = "a test value";
            c.C2 = new Composite2();
            c.C2.Value = 3;
            return c;
        }

        public void Throw()
        {
            Composite1 c = new Composite1();
            c.Value = "a test exception value";
            throw new CustomException(c);
        }
    }

}
