using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// A script that can be used programmatically.
	/// </summary>
	public class Script : BaseScript
	{
		public Script(string scriptName) : base(scriptName)
		{
		}

		public override bool Run (string[] args)
		{
			throw new System.NotImplementedException ();
		}

        static public void StartFile(string scriptFile)
        {
            var launcherScript = new Script(
                Path.GetFileNameWithoutExtension(scriptFile)
                );

            launcherScript.ExecuteScriptFile(scriptFile);
        }
	}
}
