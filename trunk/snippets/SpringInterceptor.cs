using System;
using System.Collections.Generic;

using AopAlliance.Intercept;
using Spring.Aop.Framework;

public class MyInt : IMethodInterceptor
{
	public object Invoke(IMethodInvocation invocation)
	{
	object returnValue;
		Console.WriteLine("before");
		try
		{
		returnValue = invocation.Proceed();
		}
		catch (Exception e)
		{
		Console.WriteLine("exception:" + e.Message);
		
			throw;
		}
		Console.WriteLine("after");
		return returnValue;
	}
}

public class Mc2
{
	public virtual void Test()
	{
		Console.WriteLine("hello");
	}
	
	public virtual void Test2()
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
		ProxyFactory factory = new ProxyFactory(new Mc2());
		factory.AddAdvice(new MyInt());
		Mc2 c = (Mc2)factory.GetProxy();
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