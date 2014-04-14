using System;
using System.IO;
using System.Text;

namespace SoftwareMonkeys.csAnt
{
	public class ConsoleWriter : TextWriter, IConsoleWriter
	{
        // TODO: Remove if not needed
        public StringBuilder OutputBuilder { get;set; }
        
		public string Output
        {
            get { return LogFileWriter.ToString(); }
        }

		public string LogFile { get;set; }

		public StreamWriter LogFileWriter { get;set; }

		public string ScriptName { get;set; }

		public override Encoding Encoding
		{
			get { return Encoding.ASCII; }
		}

        public bool IsVerbose { get;set; }

        public TextWriter StandardWriter { get; set; }

		public ConsoleWriter ()
		{
		}

		public ConsoleWriter(
            TextWriter standardWriter,
			string outputDirectory,
			string scriptName
		)
		{
			var logFile = Path.GetFullPath(outputDirectory)
				+ Path.DirectorySeparatorChar
				+ GetTimeStamp()
				+ "-" + scriptName
				+ ".txt";

            StandardWriter = standardWriter;

			LogFile = logFile;

			ScriptName = scriptName;
            
            OutputBuilder = new StringBuilder();
		}
		
		public override void WriteLine(string text)
		{
			if (text == null)
				text = String.Empty;

			// TODO: Increase the indent depending on the indent of the script

            // TODO: Check if needed
			//System.Console.WriteLine(text);

            StandardWriter.WriteLine (text);

			AppendOutput(text + "\n");
			
			AppendOutputFile(text + "\n");
			
			// TODO: Remove if not needed
			//base.WriteLine (text);
		}

		public override void Write(string text)
		{
			if (text == null)
				text = String.Empty;

            // TODO: Check if needed
			//System.Console.Write(text);
			
            StandardWriter.Write (text);

			AppendOutput(text);

			AppendOutputFile(text);

			// TODO: Remove if not needed
			//base.Write (text);
		}

		public void AppendOutput (string text)
		{
			if (text == null)
				text = String.Empty;

            // TODO: Check if needed. Is currently using up too much memory so should be removed if possible.
            // Currently used by some tests to access and analyse output
			//OutputBuilder.Append(text);
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
			if (disposing && LogFileWriter != null)
				LogFileWriter.Close();

			base.Dispose (disposing);
		}
	}
}

