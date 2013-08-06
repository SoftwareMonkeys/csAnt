using System;
using System.IO;
using System.Reflection;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// Used as the base of C# scripts.
	/// </summary>
	public abstract partial class BaseScript : IScript
	{
		public ConsoleWriter Console { get;set; }

		public BaseScript()
		{
			Initialize();
		}

		public abstract bool Start(string[] args);

		public virtual void Initialize()
		{
			// TODO: Inject the ConsoleWriter via constructor/creator
			if (Console == null)
				Console = new ConsoleWriter(String.Empty);
		}
	}
}
