using System;

namespace SoftwareMonkeys.csAnt
{
    public interface IScriptConstructor
    {
        void Construct(string scriptName);
        
        void Construct(string scriptName, IScript parentScript);
    }
}

