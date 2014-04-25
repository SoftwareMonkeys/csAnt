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
            get
            {
                // TODO: Check whether this code is too slow. Find a faster way to get the output.
                CloseLogFileWriter();
                var output = File.ReadAllText(LogFile);
                OpenLogFileWriter();
                return output;
            }
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
			LogFile = GetLogFilePath(outputDirectory, scriptName);

            StandardWriter = standardWriter;

			ScriptName = scriptName;
            
            OutputBuilder = new StringBuilder();
		}
		
		public override void WriteLine(string text)
		{
			if (text == null)
				text = String.Empty;
        
            StandardWriter.WriteLine (text);

			AppendOutputFile(text + "\n");
		}

		public override void Write(string text)
		{
			if (text == null)
				text = String.Empty;

            StandardWriter.Write (text);

			AppendOutputFile(text);
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
                OpenLogFileWriter();
			}

			LogFileWriter.Write(text);
		}

        public void OpenLogFileWriter()
        {
            if (File.Exists(LogFile))
                LogFileWriter = new StreamWriter(File.OpenWrite(LogFile));
            else
                LogFileWriter = File.CreateText (LogFile);

            LogFileWriter.AutoFlush = true;
        }

        public void CloseLogFileWriter()
        {
            if (LogFileWriter != null)
            {
                LogFileWriter.Close();
                LogFileWriter = null;
            }
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

        public string GetLogFilePath(string outputDirectory, string scriptName)
        {
            return Path.GetFullPath(outputDirectory)
                        + Path.DirectorySeparatorChar
                        + GetTimeStamp()
                        + "-" + scriptName
                    + ".txt";
        }

		protected override void Dispose (bool disposing)
		{
			if (disposing)
            {
                CloseLogFileWriter();
            }

			base.Dispose (disposing);
		}
	}
}

