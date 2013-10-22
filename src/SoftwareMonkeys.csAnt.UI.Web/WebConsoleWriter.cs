using System;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace SoftwareMonkeys.csAnt.UI.Web
{
	public class WebConsoleWriter : ConsoleWriter
	{
		public WebConsoleWriter (string dir, string scriptName)
			: base (dir, scriptName)
		{
		}
		
		public override void WriteLine (string text)
		{
			//Output += text + Environment.NewLine;

			base.WriteLine (text);
		}
		
		public override void Write (string text)
		{
			//Output += text;

			base.WriteLine (text);
		}

	}
}

