
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SlWcf.NonDomain
{

    [Serializable]
    public class AnotherNonDto
    {
    }

}

namespace SlWcf.Domain
{

    /// <summary>
    /// Class that should not have DTO created on Silverlight
    /// </summary>
    [Serializable]
    public class NonDto
    {

        public bool HasBanana;

        private bool _hasApple;
        public bool HasApple
        {
            get { return _hasApple; }
            protected set { _hasApple = value; }
        }

        public bool HasPear { get; protected set; }

    }

}
