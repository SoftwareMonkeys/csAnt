using System;
using System.IO;
using System.Text;

namespace SoftwareMonkeys.csAnt.UI.csAntConsole
{
	public class ConsoleWriter : TextWriter
	{
		public string Output { get;set; }

		public string OutputDirectory { get;set; }

		public override Encoding Encoding
		{
			get { return Encoding.ASCII; }
		}

		public ConsoleWriter(
			string outputDirectory
		)
		{
			OutputDirectory = outputDirectory;
		}
		
		public override void WriteLine(string text)
		{
			System.Console.WriteLine(text);

			AppendOutput(text + "\n");

			base.WriteLine (text);
		}

		public override void Write(string text)
		{
			System.Console.Write(text);
			
			AppendOutput(text);

			base.Write (text);
		}

		public void AppendOutput(string text)
		{
			Output += text;
		}
	}
}

