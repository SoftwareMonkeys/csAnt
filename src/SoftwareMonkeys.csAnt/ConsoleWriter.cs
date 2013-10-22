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

		public string ScriptName { get;set; }
		
		public IScript Script { get;set; }

		public override Encoding Encoding
		{
			get { return Encoding.ASCII; }
		}

		public ConsoleWriter ()
		{
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

			ScriptName = scriptName;
		}
		
		public override void WriteLine(string text)
		{
			if (text == null)
				text = String.Empty;

			// TODO: Increase the indent depending on the indent of the script

			System.Console.WriteLine(text);

			AppendOutput(text + "\n");
			
			AppendOutputFile(text + "\n");
			
			// TODO: Remove if not needed
			//base.WriteLine (text);
		}

		public override void Write(string text)
		{
			if (text == null)
				text = String.Empty;

			System.Console.Write(text);
			
			AppendOutput(text);

			AppendOutputFile(text);

			// TODO: Remove if not needed
			//base.Write (text);
		}

		public void AppendOutput (string text)
		{
			if (text == null)
				text = String.Empty;

			Output += text;
		}
		
		public void AppendOutputFile(string text)
		{
			if (text == null)
				text = String.Empty;

			if (!File.Exists (LogFile)) {
				var dir = Path.GetDirectoryName (LogFile);

				if (!Directory.Exists (dir))
					Directory.CreateDirectory (dir);
			}

			if (LogFileWriter == null) {

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
				+ dateTime.Second
				+ "--"
				+ dateTime.Ticks;
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing)
				LogFileWriter.Close();

			base.Dispose (disposing);
		}
	}
}

