using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

using ConfOrm;
using ConfOrm.NH;
using NHibernate.Cfg.MappingSchema;
using NUnit.Framework;

using MapDemo.Domain;
using System.Collections.Generic;

namespace MapDemo
{
    [TestFixture]
	public class Demo
	{
        [Test][Explicit]
        public void MapDomain()
        {
            var allDomainTypes =
                typeof(Base)
                    .Assembly
                    .GetTypes()
                    .Where(t => t.Namespace == typeof(Base).Namespace)
                    .ToList();

            var rootTypes =
                allDomainTypes
                    .Where(t => t.BaseType == typeof(Base))
                    .ToList();

            var domainInspector = new ObjectRelationalMapper();
            domainInspector.TablePerClassHierarchy(rootTypes);

            var mapper = new Mapper(domainInspector);

            var mappings = mapper.CompileMappingForEach(allDomainTypes);
            var xml = Serialize(mappings);

            Console.WriteLine(xml);
        }

        protected static string Serialize(IEnumerable<HbmMapping> hbmMappings)
		{
			var allXml = "";
			Directory.CreateDirectory("DemoMapping");
			foreach (var hbmMapping in hbmMappings)
			{
				var fileName = "DemoMapping\\" + GetFileName(hbmMapping);
				var xml = Serialize(hbmMapping);
				allXml += xml;
				File.WriteAllText(fileName, xml);
			}
			return allXml;
		}

		private static string GetFileName(HbmMapping hbmMapping)
		{
			var name = "MyMapping";
			var rc = hbmMapping.RootClasses.FirstOrDefault();
			if(rc!=null)
			{
				name= rc.Name;
			}
			var sc = hbmMapping.SubClasses.FirstOrDefault();
			if (sc != null)
			{
				name = sc.Name;
			}
			var jc = hbmMapping.JoinedSubclasses.FirstOrDefault();
			if (jc != null)
			{
				name = jc.Name;
			}
			var uc = hbmMapping.UnionSubclasses.FirstOrDefault();
			if (uc != null)
			{
				name = uc.Name;
			}
			return name + ".hbm.xml";
		}

		protected static string Serialize(HbmMapping hbmElement)
		{
			var setting = new XmlWriterSettings { Indent = true };
			var serializer = new XmlSerializer(typeof(HbmMapping));
			using (var memStream = new MemoryStream(2048))
			using (var xmlWriter = XmlWriter.Create(memStream, setting))
			{
				serializer.Serialize(xmlWriter, hbmElement);
				memStream.Flush();
				memStream.Position = 0;
				using (var sr = new StreamReader(memStream))
				{
					return sr.ReadToEnd();
				}
			}
		}
	}
}