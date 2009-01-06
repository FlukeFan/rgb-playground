
using System;
using System.Collections.Generic;

namespace SlWcf.Domain
{

    public enum PersonGender
    {
        Male = 1,
        Female = 2,
    }

    /// <summary>
    /// Using [Serializable] and regular property, auto-property, and field access
    /// </summary>
    [Serializable]
    public class Person
    {
        private string _name;

        protected Person()
        {
            Detail = new PersonDetail();
        }

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public int Age;
        public PersonGender Gender { get; protected set; }
        public Person Father { get; protected set; }
        protected IList<Person> Children { get; set; }
        public PersonDetail Detail { get; protected set; }
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

}
