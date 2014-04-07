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
		public IConsoleWriter ConsoleWriter { get;set; }

		public string ScriptName { get; set; }

        public int Indent { get;set; }

        public Indenter Indenter { get;set; }

		protected BaseScript ()
        {
            // The parameterless constructor shouldn't call the Construct function. It needs to be called explicitly if using this constructor.

            Constructor = new ScriptConstructor(this);
        }

		public BaseScript(string scriptName)
		{
			if (String.IsNullOrEmpty(scriptName))
				throw new ArgumentException("The script name must be provided.", "scriptName");
            
            Constructor = new ScriptConstructor(this);

            Construct(scriptName);
		}
        
        public BaseScript(string scriptName, IScript parentScript)
        {
            if (String.IsNullOrEmpty(scriptName))
                throw new ArgumentException("The script name must be provided.", "scriptName");
            
            Constructor = new ScriptConstructor(this);

            Construct(scriptName, parentScript);
        }
	}
}
