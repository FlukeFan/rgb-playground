
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Soap;
using System.ServiceModel;
using System.Xml;

namespace Demo.Domain
{

    public enum PersonGender
    {
        Male = 1,
        Female = 2,
    }

    [Serializable]
    public class Person
    {
        protected Person() { }

        public string Name { get; protected set; }
        public int Age { get; protected set; }
        public PersonGender Gender { get; protected set; }
        public Person Father { get; protected set; }
        protected IList<Person> Children { get; set; }
        public IEnumerable<Person> ChildrenEnumeration { get { return Children; } }

        public static Person CreatePerson()
        {
            Person person = new Person();
            person.Children = new List<Person>();
            return person;
        }

        public Person SetName(string name) { Name = name; return this; }
        public Person SetAge(int age) { Age = age; return this; }
        public Person SetGender(PersonGender gender) { Gender = gender; return this; }
        public Person SetFather(Person father) { Father = father; return this; }
        public Person AddChild(Person child) { Children.Add(child); return this; }

        public Person Throw()
        {
            throw new NameNotUniqueException(this);
        }
    }

    [Serializable]
    public class NameNotUniqueException : DomainException
    {
        public NameNotUniqueException(string message) : base(message)
        {
        }

        public NameNotUniqueException(Person duplicatePerson) : base("Name not unique - " + duplicatePerson.Name)
        {
            DuplicateName = duplicatePerson.Name;
        }

        public string DuplicateName { get; protected set; }
    }

    [Serializable]
    public abstract class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message) { }
    }

    [Serializable]
    public class DomainExceptionFault
    {
        public string Message;
        public string Class;
        public IDictionary<string, object> Properties;

        public static FaultException<DomainExceptionFault> CreateWcfException(DomainException exception)
        {
            DomainExceptionFault domainExceptionFault = new DomainExceptionFault();
            domainExceptionFault.Message = exception.Message;
            domainExceptionFault.Class = exception.GetType().FullName;
            domainExceptionFault.Properties = new Dictionary<string, object>();
            foreach (PropertyInfo property in exception.GetType().GetProperties())
            {
                if (typeof(DomainException).IsAssignableFrom(property.DeclaringType))
                {
                    domainExceptionFault.Properties.Add(property.Name, property.GetValue(exception, null));
                }
            }
            return new FaultException<DomainExceptionFault>(domainExceptionFault, exception.Message);
        }

        public static DomainException CreateDomainException(DomainExceptionFault domainExceptionFault)
        {
            Type exceptionType = Type.GetType(domainExceptionFault.Class);
            DomainException exception = (DomainException)exceptionType.GetConstructor(new Type[] {typeof(string)}).Invoke(new object[] {domainExceptionFault.Message});
            foreach (string propertyName in domainExceptionFault.Properties.Keys)
            {
                exceptionType.GetProperty(propertyName).SetValue(exception, domainExceptionFault.Properties[propertyName], null);
            }
            return exception;
        }
    }

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
