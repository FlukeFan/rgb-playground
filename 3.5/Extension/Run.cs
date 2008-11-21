
using System;
using System.Linq;
using System.Reflection;

namespace Demo
{

    public class Person
    {
        public string Name { get; protected set; }
    }

    public class Demo
    {

        public static void Test<T>(Func<T, bool> func)
        {
            Console.WriteLine(typeof(T));
        }

        [STAThread]
        public static void Main()
        {
            Test((Person p) => p.Name == "test");
        }

    }

}

