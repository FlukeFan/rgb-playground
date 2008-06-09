using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using NHibernate.Expression;

public class MyClass
{

	public static object MakeCopy(object sourceObject)
    {
		WL("Attempt serialise: " + sourceObject.GetType().ToString());
        object copy;

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        formatter.Serialize(stream, sourceObject);
        stream.Seek(0, SeekOrigin.Begin);

        copy = formatter.Deserialize(stream);
        stream.Close();

        return copy;
    }

	public static void Main()
	{
		try
		{
			string a = "a test";
			DetachedCriteria b = DetachedCriteria.For<MyClass>();
			
			MakeCopy(a);
			MakeCopy(b);
		}
		finally
		{
			WL("Done");
			RL();
		}
	}
	
	#region Helper methods

	private static void WL(object text, params object[] args)
	{
		Console.WriteLine(text.ToString(), args);	
	}
	
	private static void RL()
	{
		Console.ReadLine();	
	}
	
	private static void Break() 
	{
		System.Diagnostics.Debugger.Break();
	}

	#endregion
}