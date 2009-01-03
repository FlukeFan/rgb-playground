
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
                try
                {
                    throw DomainExceptionFault.CreateDomainException(de.Detail);
                }
                catch (NameNotUniqueException e)
                {
                    Assert.AreEqual("Name not unique - duplicate person", e.Message);
                    Assert.AreEqual("duplicate person", e.DuplicateName);
                }
            }
        }

        public static void Test5()
        {
            Person personIn1 = Person.CreatePerson().SetGender(PersonGender.Female).SetAge(10);
            Person personIn2 = Person.CreatePerson().SetGender(PersonGender.Female).SetAge(20);
            ServiceResult<Person> result = _service.CollatePerson(personIn1, personIn2);
            Assert.AreEqual(30, result.Result.Age);
        }

        public static void Test6()
        {
            ServiceResult result = _service.ReturnVoidOrThrow(0);
            Assert.AreEqual(true, result.IsVoid);
        }

        public static void Test7()
        {
            {
                ServiceResult result = _service.ReturnVoidOrThrow(1);
                Assert.AreEqual(true, result.IsError);
                Assert.AreEqual("Name not unique - test name", result.ExceptionMessage);
                Assert.AreEqual("Demo.Domain.NameNotUniqueException", result.ExceptionClass);
                Assert.AreEqual("test name", result.Properties["DuplicateName"]);
            }
            {
                ServiceResult result = _service.ReturnVoidOrThrow(2);
                Assert.AreEqual(true, result.IsError);
                Assert.AreEqual("silly value\r\nParameter name: choice", result.ExceptionMessage);
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
                Test5();
                Test6();
                Test7();
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
