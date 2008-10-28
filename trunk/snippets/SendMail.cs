using System;
using System.Collections.Generic;
using System.Net.Mail;

public class SendMail
{
	public static void Main()
	{
		SmtpClient client = new SmtpClient();
		client.Host = "localhost";
		
		string from = "test.client@test.exe";
		string to = "fluke_fan@hotmail.com";
		string subject = "test subject";
		string body = "test body " + DateTime.Now.ToString();
		
		client.Send(from, to, subject, body);
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