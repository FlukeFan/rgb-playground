
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace SlWcf.Services
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

            foreach (PropertyInfo property in exception.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (property.CanRead && property.CanWrite)
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

}
