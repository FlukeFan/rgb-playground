
using System;
using System.Collections.Generic;

namespace SlWcf.Domain
{

    public class NameNotUniqueException : Exception
    {

        public NameNotUniqueException(Person duplicatePerson) : base("Name not unique - " + duplicatePerson.Name)
        {
            DuplicateName = duplicatePerson.Name;
        }

        public string DuplicateName { get; protected set; }
    }

}
