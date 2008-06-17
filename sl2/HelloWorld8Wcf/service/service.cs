
using System;
using System.ServiceModel;

using Demo.Domain;

namespace Demo.Services
{

    [ServiceContract()]
    public interface ITestService
    {
        [OperationContract()]
        Composite1 GetC1();

        [OperationContract()]
        [FaultContract(typeof(CustomException))]
        void Throw();
    }

    public class TestService : ITestService
    {
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
