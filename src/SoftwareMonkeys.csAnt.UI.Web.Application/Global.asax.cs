
using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.IO;

namespace SoftwareMonkeys.csAnt.UI.Web.Application
{
	public class Global : System.Web.HttpApplication
	{

		protected virtual void Application_Start (Object sender, EventArgs e)
		{
			Environment.CurrentDirectory = Path.GetFullPath(Environment.CurrentDirectory + "/../..");

			Console.WriteLine ("Current directory: " + Environment.CurrentDirectory);
		}
		
		protected virtual void Session_Start (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_BeginRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_EndRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_AuthenticateRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_Error (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Session_End (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_End (Object sender, EventArgs e)
		{
		}
	}
}

