using System;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Tests.Unit
{
    [TestFixture]
    public class LibraryVersionerTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_SetVersion()
        {
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
            ).GrabOriginalFiles();

            var id = "FileNodes";
            var newVersion = "1000.1000.1000.1000";

            var versioner = new LibraryVersioner();

            versioner.SetVersion(id, newVersion);

            CheckNuspecFiles(id, newVersion);
            CheckCsProjFiles(id, newVersion);
            //CheckLibDir(id, newVersion); // TODO: Check if needed
        }

        public void CheckNuspecFiles(string id, string newVersion)
        {
            CheckFiles(
                id,
                newVersion,
                Directory.GetFiles(Path.GetFullPath("pkg"), "*.nuspec")
            );
        }

        public void CheckCsProjFiles(string id, string newVersion)
        {
            var list = new List<string>();

            foreach (var dir in Directory.GetDirectories(Path.GetFullPath("src")))
            {
                var files = Directory.GetFiles(dir, "*.csproj");

                if (files.Length > 0)
                    list.AddRange(files);
            }

            CheckFiles(
                id,
                newVersion,
                list.ToArray()
            );
        }

        public void CheckLibDir(string id, string newVersion)
        {
            var dir = Path.GetFullPath("lib")
                + Path.DirectorySeparatorChar
                + id + "." + newVersion;

            Assert.IsTrue(Directory.Exists(dir), "New lib directory not found.");
        }

        public void CheckFiles(string id, string newVersion, params string[] files)
        {
            Console.WriteLine("Checking files...");

            foreach (var file in files)
            {
                Console.WriteLine("  " + PathConverter.ToRelative(file));

                var content = File.ReadAllText(file);

                if (content.Contains(id))
                {
                    if (IsVerbose)
                    {
                        Console.WriteLine("    Contains ID: " + id);
                        if (content.Contains(newVersion))
                            Console.WriteLine("    Contains new version.");
                        else
                            Console.WriteLine("    Does not contain version.");
                    }

                    Assert.IsTrue(content.Contains(newVersion));
                }
                else
                {
                    if (IsVerbose)
                        Console.WriteLine("    Doesn't contain ID: " + id);
                }
            }
        }
    }
}

