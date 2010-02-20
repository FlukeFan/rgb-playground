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
        public Library OwningLibrary { get; protected set; }

        public MediaType Type { get; protected set; }
        public string Title { get; protected set; }
	}
}