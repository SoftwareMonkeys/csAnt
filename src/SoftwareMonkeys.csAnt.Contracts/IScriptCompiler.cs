using System;
namespace SoftwareMonkeys.csAnt
{
    public interface IScriptCompiler
    {
        void CompileAll();
        void Compile(params string[] scriptNames);
        void Compile(bool force, params string[] scriptNames);
    }
}

