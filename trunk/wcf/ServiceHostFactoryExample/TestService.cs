using System.ServiceModel;

namespace Shf
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        void Test(string myValue);
    }

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class TestService : ITestService
    {
        public void Test(string myValue)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("Running Test({0})", myValue));
        }
    }
}
