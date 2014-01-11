using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.IO
{
    public class DirectoryMover
    {
        public DirectoryMover ()
        {
        }

        public void Move(string source, string destination)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Moving directory: ");
            Console.WriteLine ("  " + source);
            Console.WriteLine ("To: ");
            Console.WriteLine ("  " + destination);
            Console.WriteLine ();

            var stack = new Stack<Folders>();
            stack.Push(new Folders(source, destination));

            while (stack.Count > 0)
            {
                var folders = stack.Pop();
                Directory.CreateDirectory(folders.Target);
                foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
                {
                     string targetFile = Path.Combine(folders.Target, Path.GetFileName(file));
                     if (File.Exists(targetFile)) File.Delete(targetFile);
                     File.Move(file, targetFile);
                }

                foreach (var folder in Directory.GetDirectories(folders.Source))
                {
                    stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
                }
            }
            Directory.Delete(source, true);
        }
        

        public class Folders
        {
            public string Source { get; private set; }
            public string Target { get; private set; }

            public Folders(string source, string target)
            {
                Source = source;
                Target = target;
            }
        }
    }
}

