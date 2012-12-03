using System;
using SoftwareMonkeys.Jungle.Injection;
using System.IO;
using System.Reflection;
using SoftwareMonkeys.Jungle.Diagnostics;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// Used as the base of C# scripts.
	/// </summary>
	public abstract partial class BaseScript : IScript
	{
		public IInjectionContext Injection { get;set; }

		public BaseScript()
		{
			Initialize();
		}

		public abstract bool Start(string[] args);

		public virtual void Initialize()
		{
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			var loggerCreator = new LoggerAssembler(
				path
			).Creator;

			Injection = new InjectionContextAssembler(
				path,
				loggerCreator
			).Assemble();

		}
	}
}
