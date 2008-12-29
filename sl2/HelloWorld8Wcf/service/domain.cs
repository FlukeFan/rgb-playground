
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

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

        public void Throw()
        {
            throw new NameNotUniqueException(this);
        }
    }

    [Serializable]
    public class NameNotUniqueException : ApplicationException
    {
        public NameNotUniqueException(Person duplicatePerson)
        {
            DuplicatePerson = duplicatePerson;
        }

        public Person DuplicatePerson { get; protected set; }
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
