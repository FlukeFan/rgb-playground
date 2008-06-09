using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;

public class MyClass
{
	public static void Main()
	{
		try
		{
            ResourceWriter rw = new ResourceWriter(@"C:\svn\clickOnce\HelloWorld3\DemoClickOnce\DemoClickOnce.g.resources");
            byte[] xaml = File.ReadAllBytes(@"C:\svn\clickOnce\HelloWorld3\DemoClickOnce\Pages\MyPage.xaml");
            rw.AddResource("mypage.baml", new MemoryStream(xaml));
            rw.Generate();
			WL("Done");
		}
		catch(Exception e)
		{
			Console.WriteLine("Error:\n" + e.ToString());
		}
		finally
		{
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