using System;
using SoftwareMonkeys.csAnt;

namespace SoftwareMonkeys.csAnt.Projects
{
	public abstract partial class BaseProjectScript : BaseScript
	{
		public BaseProjectScript () : base()
		{
		}

		public BaseProjectScript (string scriptName) : base(scriptName)
		{
		}

		public override void Initialize (string scriptName)
		{

			base.Initialize (scriptName);

			ImportedDirectory = GetImportedDirectory();

			Console.WriteLine ("");
			Console.WriteLine ("Imported directory:");
			Console.WriteLine (ImportedDirectory);
			Console.WriteLine ("");
		}
	}
}
