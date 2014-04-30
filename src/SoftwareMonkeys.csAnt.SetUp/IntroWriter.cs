using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.SetUp
{
    public class IntroWriter
    {
        public IntroWriter ()
        {
        }

        public void Write()
        {
            var prefix = "";
            if (Path.DirectorySeparatorChar == '/')
            {
                prefix = "sh csAnt.sh";
            }
            else
            {
                prefix = "csAnt.bat";
            }


            Console.WriteLine ("");
            Console.WriteLine ("==================================================");
            Console.WriteLine ("");
            Console.WriteLine ("Congratulations!");
            Console.WriteLine ("");
            Console.WriteLine ("csAnt is configured and ready to go.");
            Console.WriteLine ("");
            Console.WriteLine ("Path:");
            Console.WriteLine (Environment.CurrentDirectory);
            Console.WriteLine ("");
            Console.WriteLine ("You can now launch scripts...");

            Console.WriteLine ("");
            Console.WriteLine ("Syntax:");
            Console.WriteLine ("  {0} [ScriptName]", prefix);
            Console.WriteLine ("");
            Console.WriteLine ("Example:");
            Console.WriteLine ("  {0} HelloWorld", prefix);

            Console.WriteLine ("");
            Console.WriteLine ("To create a new script...");
            Console.WriteLine ("");

            Console.WriteLine ("1) Call the 'NewScript' command:");
            Console.WriteLine ("  {0} NewScript [YourScriptName]", prefix);
            Console.WriteLine ("");

            Console.WriteLine ("2) Open your script at '/scripts/[YourScriptName].cs' to add your code, then save.");
            Console.WriteLine ("");

            Console.WriteLine ("3) Launch your script:");
            Console.WriteLine ("  {0} [YourScriptName]", prefix);
            Console.WriteLine ("");
            Console.WriteLine ("Enjoy!");
            Console.WriteLine ("");
        }
    }
}

