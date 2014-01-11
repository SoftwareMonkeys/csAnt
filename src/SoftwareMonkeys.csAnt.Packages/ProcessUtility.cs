using System;
using System.IO;
using System.Diagnostics;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class ProcessUtility
    {
        static public void StartProcess (string command, string arguments)
        {
            Console.WriteLine ("");
            Console.WriteLine ("--------------------------------------------------");
            Console.WriteLine ("");
            Console.WriteLine ("Starting process:");
            Console.WriteLine (command + " " + arguments);
            Console.WriteLine ("");

            // If the command has an extension (and is therefore an actual file)
            if (Path.GetExtension (command) != String.Empty) {
                // If the file doesn't exist
                if (!File.Exists(Path.GetFullPath(command)))
                    throw new ArgumentException("Cannot find the file '" + Path.GetFullPath(command) + "'.");
            }

            // Create the process start information
            ProcessStartInfo info = new ProcessStartInfo(
                command,
                arguments
            );

            // Configure the process
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;


            info.ErrorDialog = false;

            // Start the process
            Process process = new Process();

            process.StartInfo = info;

            process.EnableRaisingEvents = true;

            // Output the data to the console
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender, DataReceivedEventArgs e)
                {
                    Console.WriteLine(e.Data);
                }
            );
            
            // Output the errors to the console
            process.ErrorDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender, DataReceivedEventArgs e)
                {
                    Console.WriteLine(e.Data);
                }
            );

            process.Start();

            process.BeginOutputReadLine();

            process.WaitForExit();

            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("");
            
        }
    }
}

