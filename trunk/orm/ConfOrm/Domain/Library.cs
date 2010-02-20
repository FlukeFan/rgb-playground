using System.Collections.Generic;

namespace MapDemo.Domain
{
	public abstract class Library : Base
	{
        public virtual string Name { get; protected set; }

        public IEnumerable<Media> Media { get; protected set; }
	}

    public class SpecialLibrary : Library
    {
        public virtual string SpecialName { get; protected set; }
    }

    public class DetailedLibrary : Library
    {
        public virtual string Detail { get; protected set; }
    }
}