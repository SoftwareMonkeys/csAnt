using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class BaseTestScriptSetUpper : BaseScriptSetUpper
    {
        public BaseTestScriptSetUpper (IScript script) : base(script)
        {
        }

        public override void SetUp ()
        {
            base.SetUp ();

            var tmpDir = Script.GetTmpDir ();

            // If the script hasn't been relocated for the test, then relocated it
            if (Script.ParentScript != null && Script.ParentScript.CurrentDirectory == Script.CurrentDirectory)
            {
                if (Script.IsVerbose)
                    Console.WriteLine ("CurrentDirectory is the same as ParentScript.CurrentDirectory. Relocating.");
                
                Script.Relocator.Relocate (tmpDir);

                CreateNodes();
            }
            else if (Script.CurrentDirectory == Script.OriginalDirectory)
            {
                if (Script.IsVerbose)
                    Console.WriteLine ("CurrentDirectory is the same as OriginalDirectory. Relocating.");

                Script.Relocator.Relocate (tmpDir);

                CreateNodes();
            }
            else
            {
                if (Script.IsVerbose)
                {
                    Console.WriteLine("Already in a new location. Skipping relocation. ");
                }
            }
        }

        public void CreateNodes ()
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Creating nodes for test location...");
                Console.WriteLine ("  Node manager type: " + Script.Nodes.GetType ().Name);
                Console.WriteLine ("");

                // Create the nodes necessary to function in the temporary test location
                Script.Nodes.CreateNodes ();
            }
        }
    }
}

