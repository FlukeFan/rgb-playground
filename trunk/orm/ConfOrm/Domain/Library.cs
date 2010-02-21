using System.Collections.Generic;
using ConfOrm.NH;

namespace MapDemo.Domain
{
	public abstract class Library : Base
	{
		public virtual string Name { get; protected set; }
		public virtual string Description { get; protected set; }

		public IEnumerable<Media> Media { get; protected set; }
	}

	public class SpecialLibrary : Library
	{
		public static void CustomiseMapping(IPersistentClassCustomizer<SpecialLibrary> customizer)
		{
			customizer.Property(p => p.SpecialName, m => m.Column(cm => cm.Length(1000)));
		}

		public virtual string SpecialName { get; protected set; }
	}

	public class DetailedLibrary : Library
	{
		public static void CustomiseMapping(IPersistentClassCustomizer<DetailedLibrary> customizer)
		{
			customizer.Property(p => p.Detail, m => m.Column(cm => cm.Length(4000)));
		}

		public virtual string Detail { get; protected set; }
	}
}