using System;
using SoftwareMonkeys.FileNodes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SoftwareMonkeys.csAnt.Contracts.cs;

namespace SoftwareMonkeys.csAnt
{
	public partial interface IScript : IDisposable
	{
		#region Properties
		string ScriptName { get;set; }

		bool IsVerbose { get;set; }

		int Indent { get; set; }
		#endregion

        IPackageManager Packages { get;set; }
    }
}
