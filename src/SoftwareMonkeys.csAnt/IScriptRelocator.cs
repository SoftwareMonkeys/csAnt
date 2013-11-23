using System;

namespace SoftwareMonkeys.csAnt
{
    public interface IScriptRelocator
    {
        void Relocate(string path);

        void Return();
    }
}

