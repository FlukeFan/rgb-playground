using System;
using System.Collections.Generic;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

public class Controller
{
	private void DoSomeCrap()
	{
		int i = 200;
		for (int j=0; j<i; j++)
			for (int k=0; k<i; k++)
				;
	}
	
	public virtual void OnInit()
	{
		DoSomeCrap();
	}
	
	public virtual void OnLoad()
	{
		DoSomeCrap();
	}
	
	public virtual void OnPreRender()
	{
		DoSomeCrap();
	}
}

public class ManualDecorate : Controller
{
	public override void OnInit()
	{
		MyClass.CallCount += 1;
		base.OnInit();
	}

	public override void OnLoad()
	{
		MyClass.CallCount += 1;
		base.OnLoad();
	}
	
	public override void OnPreRender()
	{
		MyClass.CallCount += 1;
		base.OnPreRender();
	}
}

public class MyInterceptor : IInterceptor
{
	public void Intercept(IInvocation invocation)
	{
		MyClass.CallCount += 1;
		invocation.Proceed();
	}
}

public class MyClass
{
	public static ProxyGenerator pg = new ProxyGenerator();
	public static IInterceptor inter = new MyInterceptor();
	
	public static int CreateCount = 0;
	public static int CallCount = 0;
	
	public static void RunSnippet()
	{
		Init();
		
		CreateCount = 0;
		CallCount = 0;
		
		RunFactory(CreateConcrete);
		
		WL("Concrete create count = " + CreateCount);
		WL("Concrete call count = " + CallCount);
		
		CreateCount = 0;
		CallCount = 0;
		
		RunFactory(CreateIntercepted);
		
		WL("Intercepted create count = " + CreateCount);
		WL("Intercepted call count = " + CallCount);
	}
	
	public static void Init()
	{
		int initCount = 20;

		for (int i = 0; i < initCount; i++)
		{
			var controller = CreateConcrete();
			Run(controller);
		}

		for (int i = 0; i < initCount; i++)
		{
			var controller = CreateIntercepted();
			Run(controller);
		}
	}
	
	public static void Run(Controller controller)
	{
		controller.OnInit();
		controller.OnLoad();
		controller.OnPreRender();
	}
	
	public static void RunFactory(Func<Controller> factory)
	{
		var end = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1000);
		
		while (DateTime.Now < end)
		{
			var controller = factory();
			CreateCount += 1;
			Run(controller);
		}
		
	}
	
	private static Type concreteClass = null;
	private static Type interceptedClass = null;
	
	public static Controller CreateConcrete()
	{
		if (concreteClass == null)
		{
			concreteClass = typeof(ManualDecorate);
		}
		
		return (Controller)Activator.CreateInstance(concreteClass, null);
	}
	
	public static Controller CreateIntercepted()
	{
		return pg.CreateClassProxy<Controller>(inter);
/*		if (interceptedClass == null)
		{
			interceptedClass = pg.CreateClassProxy<Controller>(inter).GetType();
		}
		
		return (Controller)Activator.CreateInstance(interceptedClass, null);*/
	}
	
	#region Helper methods
	
	public static void Main()
	{
		try
		{
			RunSnippet();
		}
		catch (Exception e)
		{
			string error = string.Format("---\nThe following error occurred while executing the snippet:\n{0}\n---", e.ToString());
			Console.WriteLine(error);
		}
		finally
		{
			Console.Write("Press any key to continue...");
			Console.ReadKey();
		}
	}

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