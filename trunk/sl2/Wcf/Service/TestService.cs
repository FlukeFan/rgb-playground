
using System;
using System.Collections.Generic;
using System.ServiceModel;

using SlWcf.Domain;

namespace SlWcf.Services
{

    [ServiceContract]
    public interface ITestService
    {

        [OperationContract]
        ServiceResult<Person> CollatePerson(Person person1, Person person2);

        [OperationContract]
        ServiceResult ReturnVoidOrThrow(int choice);

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
                else if (choice == 2)
                {
                    throw new ArgumentException("silly value", "choice");
                }
                else
                {
                    throw new System.ComponentModel.LicenseException(typeof(TestService));
                }
            }
            catch(Exception e)
            {
                return ServiceResult.Error(e);
            }
        }

    }

}
