using System;

namespace SoftwareMonkeys.csAnt.UI.Web
{
	public class WebLauncherScript : BaseScript
	{
		public WebLauncherScript ()
		{

		}

		public override void Initialize (string scriptName)
		{
			base.Initialize (scriptName);

			//Console = new WebConsoleWriter("logs", scriptName); // Move term "logs" to somewhere more easily configured
		}
		#region implemented abstract members of SoftwareMonkeys.csAnt.BaseScript
		public override bool Start (string[] args)
		{
			throw new NotImplementedException();
		}
		#endregion

	}
}

