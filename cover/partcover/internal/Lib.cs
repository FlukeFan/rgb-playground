
using System;

namespace Example
{

    public class Lib
    {

        public static string MyValue
        {
            internal get { return "returned from get_MyValue"; }
            set { Console.WriteLine("Called set_MyValue"); }
        }
        
        public static string Access()
        {
            return MyValue;
        }

    }


}

