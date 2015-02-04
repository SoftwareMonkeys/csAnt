using System;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.IO;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt.SetUp.Deploy.Launch
{
    public class SetUpFromLocalScriptLauncher : BaseSetUpLauncher
    {
        public ProcessStarter Starter { get;set; }

        public bool Clone { get;set; }

        public string CloneSource { get;set; }

        public bool Import { get;set; }

        public string ImportSource { get;set; }

        public SetUpFromLocalScriptLauncher ()
        {
            Starter = new ProcessStarter();
        }
        
        public override void Launch(string installationPath)
        {
            // TODO: Should sourcePath be set to Environment.CurrentDirectory?

            Console.WriteLine("Launching setup from local script...");
            Console.WriteLine("");
            Console.WriteLine("Installation path:");
            Console.WriteLine(installationPath);

            var name = "csAnt-setupfromlocal";

            var arguments = GetArguments(String.Empty, installationPath);

            // TODO: Should the project directory be set to Environment.CurrentDirectory?
            if (SoftwareMonkeys.csAnt.Processes.Platform.IsLinux)
                Starter.Start("sh", name + ".sh", arguments);
            else
                Starter.Start("cscript", name + ".vbs", arguments);
        }

        public override void Launch(string sourcePath, string installationDirectory)
        {
            // TODO: Should sourcePath be set to Environment.CurrentDirectory?

            Console.WriteLine("Launching setup from local script...");
            Console.WriteLine("");
            Console.WriteLine("Installation directory:");
            Console.WriteLine(installationDirectory);

            var name = "csAnt-setupfromlocal";

            var arguments = GetArguments(sourcePath, installationDirectory);

            // TODO: Should the project directory be set to Environment.CurrentDirectory?
            if (SoftwareMonkeys.csAnt.Processes.Platform.IsLinux)
                Starter.Start("sh", name + ".sh", arguments);
            else
                Starter.Start("cscript", name + ".vbs", arguments);
        }

        public string[] GetArguments(string sourcePath, string destinationPath)
        {
            var list = new List<string>();

            if (!String.IsNullOrEmpty(sourcePath))
                list.Add(sourcePath);

            if (!String.IsNullOrEmpty(destinationPath))
                list.Add("-destination=" + destinationPath);
            
            if (Clone)
            {
                if (String.IsNullOrEmpty(CloneSource))
                    list.Add("-clone");
                else
                    list.Add("-clone=" + CloneSource);
            }

            if (Import)
            {
                if (String.IsNullOrEmpty(ImportSource))
                    list.Add("-import");
                else
                    list.Add("-import=" + ImportSource);
            }
            return list.ToArray();
        }
    }
}

