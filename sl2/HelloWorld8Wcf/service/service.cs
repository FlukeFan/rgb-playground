
using System;
using System.ServiceModel;

using Demo.Domain;

namespace Demo.Services
{

    [ServiceContract()]
    public interface ITestService
    {

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
