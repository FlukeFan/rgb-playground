
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SlWcf.Domain
{

    /// <summary>
    /// Using [DataContract] and regular property, auto-property, and field access
    /// </summary>
    [DataContract]
    public class PersonDetail
    {

        [DataMember]
        public bool HasXbox;

        [DataMember] private bool _hasCar;

        public bool HasCar
        {
            get { return _hasCar; }
            protected set { _hasCar = value; }
        }

        [DataMember]
        public bool HasDog { get; protected set; }

    }

}
