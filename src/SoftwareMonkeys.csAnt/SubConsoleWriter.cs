using System;

namespace SoftwareMonkeys.csAnt
{
	public class SubConsoleWriter : ConsoleWriter
	{
		public ConsoleWriter ParentWriter { get;set; }

		public SubConsoleWriter (ConsoleWriter parentWriter, string scriptName)
		{
			ParentWriter = parentWriter;

			ScriptName = scriptName;
		}

		public override void WriteLine (string text)
		{
			if (text == null)
				text = String.Empty;

			// TODO: Increase the indent depending on the indent of the script

			if (ParentWriter != null)
				ParentWriter.WriteLine (text);
			else {
				System.Console.WriteLine (text);
				AppendOutputFile(text);
			}

			AppendOutput(text + "\n");
		}

		public override void Write (string text)
		{
			if (text == null)
				text = String.Empty;

			if (ParentWriter != null)
				ParentWriter.Write (text);
			else {
				System.Console.Write (text);
				AppendOutputFile(text);
			}

			AppendOutput(text);
		}
	}
}

