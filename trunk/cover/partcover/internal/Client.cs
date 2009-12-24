
using System;

namespace Example
{

    public class Client
    {

        public static void Main(string[] args)
        {
            Lib.MyValue = "a value";
            Console.WriteLine(Lib.Access());
        }

    }


}

