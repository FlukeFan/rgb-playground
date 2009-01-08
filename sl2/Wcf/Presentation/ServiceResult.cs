
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SlWcf.Services
{

    [DataContract]
    public class ServiceResult
    {
        [DataMember] public bool IsVoid { get; set; }
        [DataMember] public bool IsError { get; set; }
        [DataMember] public string ExceptionMessage { get; set; }
        [DataMember] public string ExceptionClass { get; set; }
        [DataMember] public IDictionary<string, object> Properties { get; set; }
    }

    [DataContract]
    public class ServiceResult<T> : ServiceResult
    {
        [DataMember]
        public T Result { get; set; }
    }

}

