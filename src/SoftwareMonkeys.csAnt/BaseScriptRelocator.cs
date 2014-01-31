using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public class BaseScriptRelocator : IScriptRelocator
    {
        public IScript Script { get;set; }

        bool HasReturned = false;

        bool AutoReturn = true;

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
                Console.WriteLine ("Relocating to directory:");
                Console.WriteLine (workingDirectory);
                Console.WriteLine ("");
            }
            
            // TODO: Clean up
            Script.CurrentDirectory = Path.GetFullPath (workingDirectory);
            //CurrentNode = GetCurrentNode ();

            // Clear the import staging directory and it will refresh when it's needed
            Script.ImportStagingDirectory = "";

            Script.Nodes.Refresh();
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
            if (AutoReturn)
                Return ();
        }
    }
}

