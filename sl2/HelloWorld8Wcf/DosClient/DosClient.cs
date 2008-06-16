
using System;
using System.Collections.Generic;
using System.ServiceModel;

using NUnit.Framework;

using Demo.Services;

namespace Demo
{

    public class DosClient
    {

        private static ITestService _service;

        public static void Test1()
        {
            Composite1 c1 = _service.GetC1();
            Assert.AreEqual("a test value", c1.Value);
            Assert.AreEqual(3, c1.C2.Value);
        }

        public static void Test2()
        {
            try
            {
                _service.Throw();
                Assert.Fail("no exception thrown");
            }
            catch (FaultException fe)
            {
                Console.WriteLine(fe.ToString());
            }
        }

        public static int Main(string[] args)
        {
            try
            {
                _service = new ChannelFactory<ITestService>("TestService").CreateChannel();
                Test1();
                Test2();
                Console.WriteLine("Done");
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }

    }

}
