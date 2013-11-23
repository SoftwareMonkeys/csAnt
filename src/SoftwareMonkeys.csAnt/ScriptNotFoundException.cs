using System;

namespace SoftwareMonkeys.csAnt
{
    public class ScriptNotFoundException : Exception
    {
        public ScriptNotFoundException (string scriptName) : base("No script was found with the name '" + scriptName + "'.") 
        {
        }
    }
}

