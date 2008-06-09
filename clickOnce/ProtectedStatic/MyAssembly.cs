
using System;

namespace StaticTest
{

    public class MyClass
    {
        private string _myProperty = "original value";

        public string MyProperty
        {
            get { return _myProperty; }
            protected set { _myProperty = value; }
        }

    }

    public class MySubClass : MyClass
    {
    
        public static void ModiyProperty(MyClass instance, string newValue)
        {
            instance.MyProperty = newValue;
        }

    }

    public class Run
    {

        // Entry point method
        [STAThread]
        public static void Main()
        {
            MyClass myClass = new MyClass();
            MySubClass.ModiyProperty(myClass, "new value");
            Console.WriteLine("Hello world: " + myClass.MyProperty);
        }

    }
}