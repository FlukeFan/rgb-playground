using System.Collections.Generic;

namespace MapDemo.Domain
{
	public enum MediaType
	{
		Book = 1,
		Dvd = 2,
		CD = 3,
	}

	public class Media : Base
	{
		public virtual Library OwningLibrary { get; protected set; }

		public virtual MediaType Type { get; protected set; }
		public virtual string Title { get; protected set; }
		public virtual string Description { get; protected set; }
	}
}