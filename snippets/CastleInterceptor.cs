using System;
using System.Collections.Generic;

using Castle.Core;
using Castle.Core.Interceptor;
using Castle.Windsor;

public class MyInt : IInterceptor
{
	public void Intercept(IInvocation invocation)
	{
		Console.WriteLine("before");
		try
		{
		invocation.Proceed();
		}
		catch (Exception e)
		{
		Console.WriteLine("exception:" + e.Message);
		
			throw;
		}
		Console.WriteLine("after");
	}
}

public class Mc2
{
	public virtual void Test()
	{
		Console.WriteLine("hello");
	}
	
	public void Test2()
	{
		throw new Exception("a test");
	}
	
}

public class MyClass
{
	public static void Main()
	{
	try
	{
		IWindsorContainer wc = new WindsorContainer();
		
		wc.AddComponent<Mc2>();
		wc.AddComponent<MyInt>();
		
		//wc.Kernel.GetHandler(typeof(Mc2)).ComponentModel.InspectionBehavior = PropertiesInspectionBehavior.All;
		
		wc.Kernel.GetHandler(typeof(Mc2))
			.ComponentModel.Interceptors
				.Add(new InterceptorReference(typeof(MyInt)));
				
		//WL(wc.Kernel.ProxyFactory);
		
		Mc2 c = wc.Resolve<Mc2>();
		c.Test();
		c.Test2();
		RL();
		}
		catch(Exception e)
		{
			WL(e);
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