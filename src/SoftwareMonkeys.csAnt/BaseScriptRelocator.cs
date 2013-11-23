using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class BaseScriptRelocator
    {
        public IScript Script { get;set; }

        bool HasReturned = false;

        public BaseScriptRelocator (IScript script)
        {
            Script = script;
        }

        public void Relocate ()
        {
            var tmpDir = Script.GetTmpDir ();
            Relocate (tmpDir);
        }

        public void Relocate (string workingDirectory)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Relocating to test directory:");
                Console.WriteLine (workingDirectory);
                Console.WriteLine ("");
            }
            Script.Relocate(workingDirectory);
        }

        public void Return ()
        {
            if (!HasReturned) {
                Script.Relocate (Script.OriginalDirectory);
                HasReturned = true;
            }
        }

        public void Dispose()
        {
            Return ();
        }
    }
}

