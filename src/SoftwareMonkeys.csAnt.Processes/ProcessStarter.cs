using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;


namespace SoftwareMonkeys.csAnt.Processes
{
    public class ProcessStarter
    {
        public bool IsError { get;set; }

        public ProcessStarter ()
        {
        }

        public Process Start(string command)
        {
            if (command.Contains(" "))
            {
                var cmd = String.Empty;
                var arguments = new string[]{};
                var list = new List<string>(command.Split(' '));
                cmd = list[0];
                list.RemoveAt(0);
                arguments = list.ToArray();
                return Start(cmd, arguments);
            }
            else
            {
                return Start(command, new string[]{});
            }
        }
        /// <summary>
        /// Starts/executes a process in the current thread.
        /// </summary>
        /// <param name='command'></param>
        /// <param name='arguments'></param>
        public Process Start(string command, params string[] arguments)
        {
            arguments = FixArguments(arguments);

            return Start(command, String.Join(" ", arguments));
        }
        
        /// <summary>
        /// Starts/executes a process in the current thread.
        /// </summary>
        /// <param name='command'></param>
        /// <param name='arguments'></param>
        public Process Start (string command, string arguments)
        {
            Console.WriteLine ("");
            Console.WriteLine ("--------------------");
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
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;

            // TODO: Remove if not needed
            info.ErrorDialog = true;

            // Start the process
            Process process = new Process();

            process.StartInfo = info;

            process.EnableRaisingEvents = true;

            var c = Console.Out;

            // Output the errors to the console
            process.ErrorDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender, DataReceivedEventArgs e)
                {
                    IsError = true;

                    Console.SetOut (c);
                    c.WriteLine(e.Data);
                }
            );

            // Output the data to the console
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender, DataReceivedEventArgs e)
                {
                    Console.SetOut (c);
                    c.WriteLine(e.Data);
                }
            );

            try
            {
                process.Start();
    
                // TODO: Clean up
                /*process.BeginOutputReadLine();

                while (!process.StandardOutput.EndOfStream) {
                    string line = process.StandardOutput.ReadLine();
                    Console.WriteLine (line);
                }

                process.BeginOutputReadLine();
                
                while (!process.StandardError.EndOfStream) {
                    string line = process.StandardOutput.ReadLine();
                    Console.WriteLine (line);
                }*/

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
            catch (Exception ex)
            {
                IsError = true;
                Console.WriteLine ("Error starting process.", ex);
            }


            Console.WriteLine("");
            Console.WriteLine("--------------------");
            Console.WriteLine("");

            return process;
        }

        public string[] FixArguments(string[] arguments)
        {
            List<string> argsList = new List<string>();
            
            if (arguments != null && arguments.Length > 0)
                argsList.AddRange(arguments);

            for (int i = 0; i < argsList.Count; i++)
            {
                if (!String.IsNullOrEmpty (argsList[0]))
                {
                    if (
                        argsList[0].IndexOf(" ") > -1
                        && argsList[0].IndexOf("\"") != 0
                    )
                    {
                        argsList[0] = @"""" + argsList[0] + @"""";
                    }
                }
            }

            return argsList.ToArray();
        }


    }
}

