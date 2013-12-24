using System;

namespace SoftwareMonkeys.csAnt
{
    public interface IFilesGrabber
    {
        void GrabOriginalScripts (params string[] scriptNames);

        void GrabOriginalScriptingFiles ();
        
        void GrabOriginalFiles ();

        void GrabOriginalFiles (params string[] patterns);
    }
}

