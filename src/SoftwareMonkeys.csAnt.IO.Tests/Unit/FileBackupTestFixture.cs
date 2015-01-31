using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.IO.Tests
{
    [TestFixture]
    public class FileBackupTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_Backup()
        {
            new FilesGrabber (OriginalDirectory, CurrentDirectory)
                .GrabOriginalFiles ();

            var testFile = Path.GetFullPath ("test.txt");

            File.WriteAllText (testFile, "Test content");

            var filePath = new FileBackup ().Backup (testFile);

            Console.WriteLine ("Backup file path: " + filePath);

            var ext = Path.GetExtension (filePath);

            Assert.AreEqual (".bak", ext, ".bak extension wasn't applied");
        }
    }
}

