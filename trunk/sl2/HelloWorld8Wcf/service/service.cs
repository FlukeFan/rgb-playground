
using System;

namespace Demo.Services
{

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

    public class Composite2
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

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

    interface ITestService
    {
        Composite1 GetC1();

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
