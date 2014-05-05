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
            var existingVersion = versioner.GetVersion("FileNodes");

            versioner.SetVersion(id, newVersion);

            CheckNuspecFiles(id, existingVersion, newVersion);
            CheckCsProjFiles(id, existingVersion, newVersion);
            CheckSourceDir(id, existingVersion, newVersion);
        }

        public void CheckNuspecFiles(string id, string existingVersion, string newVersion)
        {
            CheckFiles(
                id,
                existingVersion,
                newVersion,
                Directory.GetFiles(Path.GetFullPath("pkg"), "*.nuspec")
            );
        }

        public void CheckCsProjFiles(string id, string existingVersion, string newVersion)
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
                existingVersion,
                newVersion,
                list.ToArray()
            );
        }

        public void CheckLibDir(string id, string existingVersion, string newVersion)
        {
            var dir = Path.GetFullPath("lib")
                + Path.DirectorySeparatorChar
                + id + "." + newVersion;

            Assert.IsTrue(Directory.Exists(dir), "New lib directory not found.");
        }

        public void CheckSourceDir(string id, string existingVersion, string newVersion)
        {
            var srcDir = Path.GetFullPath("src");

            var list = new List<string>();

            foreach (var dir in Directory.GetDirectories(srcDir))
            {
                var files = Directory.GetFiles(dir, "*.cs");

                if (files.Length > 0)
                    list.AddRange(files);
            }

            CheckFiles(
                id,
                existingVersion,
                newVersion,
                list.ToArray()
            );
        }

        public void CheckFiles(string id, string existingVersion, string newVersion, params string[] files)
        {
            Console.WriteLine("Checking files...");

            foreach (var file in files)
            {
                Console.WriteLine("  " + PathConverter.ToRelative(file));

                var content = File.ReadAllText(file);

                Assert.IsFalse(content.Contains(existingVersion.ToString()));

                if (content.Contains(id + ".")
                    || content.Contains(id + "\""))
                {
                    if (IsVerbose)
                    {
                        Console.WriteLine("    Contains ID: " + id);
                    }

                    bool doesContain = content.Contains(newVersion);

                    if (doesContain)
                    {
                        if (IsVerbose)
                            Console.WriteLine("    Contains new version.");
                    }
                    else
                    {
                        Console.WriteLine("    Missing new version.");

                        Assert.Fail("    New version not found.");
                    }
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

