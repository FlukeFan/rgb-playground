
using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Demo.Domain
{

    [DataContract]
    public class Composite1
    {
        [DataMember] private string _value;
        [DataMember] private Composite2 _c2;

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

    [DataContract]
    public class Composite2
    {
        [DataMember] private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    [DataContract]
    public class CustomException // : ApplicationException
    {
        [DataMember] private Composite1 _c1;

        public CustomException(Composite1 c1)
        {
            _c1 = c1;
        }

        public Composite1 C1
        {
            get { return _c1; }
        }
    }

}
