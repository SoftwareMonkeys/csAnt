using System;
using System.IO;
using System.Text;

namespace SoftwareMonkeys.csAnt
{
	public class ConsoleWriter : TextWriter
	{
		public string Output { get;set; }

		public string LogFile { get;set; }

		public StreamWriter LogFileWriter { get;set; }

		public override Encoding Encoding
		{
			get { return Encoding.ASCII; }
		}

		public ConsoleWriter(
			string outputDirectory,
			string scriptName
		)
		{
			var logFile = Path.GetFullPath(outputDirectory)
				+ Path.DirectorySeparatorChar
				+ GetTimeStamp()
				+ "-" + scriptName
				+ ".txt";

			LogFile = logFile;
		}
		
		public override void WriteLine(string text)
		{
			System.Console.WriteLine(text);

			AppendOutput(text + "\n");
			
			// TODO: Remove if not needed
			//base.WriteLine (text);
		}

		public override void Write(string text)
		{
			System.Console.Write(text);
			
			AppendOutput(text);

			// TODO: Remove if not needed
			//base.Write (text);
		}

		public void AppendOutput (string text)
		{
			Output += text;

			if (!File.Exists (LogFile))
			{
				var dir = Path.GetDirectoryName(LogFile);

				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				LogFileWriter = File.CreateText (LogFile);

				LogFileWriter.AutoFlush = true;
			}

			LogFileWriter.Write(text);
		}

		public string GetTimeStamp()
		{
			var dateTime = DateTime.Now;

			return dateTime.Year
				+ "-"
				+ dateTime.Month
				+ "-"
				+ dateTime.Day
				+ "--"
				+ dateTime.Hour
				+ "-"
				+ dateTime.Minute
				+ "-"
				+ dateTime.Second;
		}
	}
}

