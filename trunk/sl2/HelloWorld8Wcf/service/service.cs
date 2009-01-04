
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;
using System.ServiceModel;

using Demo.Domain;

namespace Demo.Services
{

    [DataContract]
    public class ServiceResult
    {

        protected ServiceResult()
        {
        }

        [DataMember] public bool IsVoid { get; protected set; }
        [DataMember] public bool IsError { get; protected set; }
        [DataMember] public string ExceptionMessage { get; protected set; }
        [DataMember] public string ExceptionClass { get; protected set; }
        [DataMember] public IDictionary<string, object> Properties { get; protected set; }

        public static ServiceResult Void
        {
            get
            {
                ServiceResult serviceResult = new ServiceResult();
                serviceResult.IsVoid = true;
                return serviceResult;
            }
        }

        public static ServiceResult Error(Exception exception)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.IsError = true;
            serviceResult.ExceptionMessage = exception.Message;
            serviceResult.ExceptionClass = exception.GetType().FullName;
            serviceResult.Properties = new Dictionary<string, object>();

            foreach (PropertyInfo property in exception.GetType().GetProperties())
            {
                if (typeof(DomainException).IsAssignableFrom(property.DeclaringType))
                {
                    serviceResult.Properties.Add(property.Name, property.GetValue(exception, null));
                }
            }

            return serviceResult;
        }

    }

    [DataContract]
    public class ServiceResult<T> : ServiceResult
    {
        protected ServiceResult() { }

        [DataMember]
        public T Result { get; protected set; }

        public static ServiceResult<T> Return(T result)
        {
            ServiceResult<T> serviceResult = new ServiceResult<T>();
            serviceResult.Result = result;
            return serviceResult;
        }

    }

    [ServiceContract()]
    public interface ITestService
    {

        [OperationContract()]
        ServiceResult<Person> CollatePerson(Person person1, Person person2);

        [OperationContract()]
        ServiceResult ReturnVoidOrThrow(int choice);

        [OperationContract()]
        Person GetPersonGraph();

        [OperationContract()]
        [FaultContract(typeof(DomainExceptionFault))]
        Person GetPersonThrowError();

        [OperationContract()]
        Composite1 GetC1();

        [OperationContract()]
        [FaultContract(typeof(CustomException))]
        void Throw();
    }

    public class TestService : ITestService
    {

        public ServiceResult<Person> CollatePerson(Person person1, Person person2)
        {
            Person person =
                Person.CreatePerson()
                    .SetGender(PersonGender.Male)
                    .SetAge(person1.Age + person2.Age);

            return ServiceResult<Person>.Return(person);
        }

        public ServiceResult ReturnVoidOrThrow(int choice)
        {
            try
            {
                if (choice == 0)
                {
                    return ServiceResult.Void;
                }
                else if (choice == 1)
                {
                    Person.CreatePerson().SetName("test name").Throw();
                    return ServiceResult.Void;
                }
                else
                {
                    throw new ArgumentException("silly value", "choice");
                }
            }
            catch(Exception e)
            {
                return ServiceResult.Error(e);
            }
        }

        public Person GetPersonGraph()
        {
            Person father =
                Person.CreatePerson()
                    .SetName("father")
                    .SetAge(50)
                    .SetGender(PersonGender.Male);

            Person child1 =
                Person.CreatePerson()
                    .SetName("daughter")
                    .SetAge(1)
                    .SetGender(PersonGender.Female);

            Person child2 =
                Person.CreatePerson()
                    .SetName("son")
                    .SetAge(2)
                    .SetGender(PersonGender.Male);

            Person person =
                Person.CreatePerson()
                    .SetName("test person")
                    .SetAge(30)
                    .SetGender(PersonGender.Female)
                    .SetFather(father)
                    .AddChild(child1)
                    .AddChild(child2);

            return person;
        }

        public Person GetPersonThrowError()
        {
            try
            {
                return
                    Person.CreatePerson()
                        .SetName("duplicate person")
                        .Throw();
            }
            catch (DomainException e)
            {
                throw DomainExceptionFault.CreateWcfException(e);
            }
        }

        public Composite1 GetC1()
        {
            Composite1 c = new Composite1();
            c.Value = "a test value";
            c.C2 = new Composite2();
            c.C2.Value = 3;
            return c;
        }

        public void Throw()
        {
            Composite1 c = new Composite1();
            c.Value = "a test exception value";
            throw new FaultException<CustomException>(new CustomException(c));
        }
    }

}
