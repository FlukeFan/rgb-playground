
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using NUnit.Framework;

using Demo.Domain;
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
            catch (FaultException<CustomException> fe)
            {
                Assert.AreEqual("a test exception value", fe.Detail.C1.Value);
            }
        }

        public static void Test3()
        {
            Person person = _service.GetPersonGraph();
            Assert.AreEqual("test person", person.Name);
            Assert.AreEqual(30, person.Age);

            Assert.AreEqual("father", person.Father.Name);
            Assert.AreEqual("son", person.ChildrenEnumeration.Skip(1).Take(1).First().Name);
        }

        public static void Test4()
        {
            try
            {
                _service.GetPersonThrowError();
                Assert.Fail("no exception thrown");
            }
            catch (FaultException<DomainExceptionFault> de)
            {
                Console.WriteLine("Exception caught: "
                    + "\n\t" + de.Detail.Message
                    + "\n\t" + de.Detail.Class
                    + "\n\t" + de.Detail.SerialisedException);
/*                try
                {
                    throw DomainExceptionFault.CreateDomainException(de.Detail);
                }
                catch (NameNotUniqueException e)
                {
                    Assert.AreEqual("a test exception value", e.DuplicatePerson.Name);
                }*/
            }
        }

        public static int Main(string[] args)
        {
            try
            {
                _service = new ChannelFactory<ITestService>("TestService").CreateChannel();
                Test1();
                Test2();
                Test3();
                Test4();
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
