using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.UI.csAntConsole
{
	public class ConsoleTextWriter : TextWriter
	{
		public string Output { get;set; }
		
		public void WriteLine(string text)
		{
			System.Console.WriteLine(text);

			AppendOutput(text + "\n");
		}

		public void Write(string text)
		{
			System.Console.Write(text);
			
			AppendOutput(text);
		}

		public void AppendOutput(string text)
		{
			Output += text;
		}
	}
}

