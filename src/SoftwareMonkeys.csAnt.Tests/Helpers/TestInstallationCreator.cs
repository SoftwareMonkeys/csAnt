using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests.Helpers
{
    public class TestInstallationCreator
    {
        public string OriginalDirectory { get;set; }

        public string WorkingDirectory { get; set; }

        public string InstallationDirectoryName = "TestInstallation";

        private string installationPath;
        public string InstallationPath
        {
            get {
                if (String.IsNullOrEmpty (installationPath))
                    installationPath = CreateTestInstallationPath ();
                return installationPath; }
        }

        public TestInstallationCreator (
            string originalDirectory,
            string workingDirectory
            )
        {
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
        }

        public string CreateBlank()
        {
            var path = CreateTestInstallationPath ();

            DirectoryChecker.EnsureDirectoryExists (path);

            return path;
        }

        public void GrabSetupFiles()
        {
            new FileCopier(
                OriginalDirectory,
                WorkingDirectory
            ).Copy(
                "csAnt-SetUp.exe"
            );
        }

        public string CreateAndGrabFiles()
        {
            throw new NotImplementedException ();
        }
        
        public string CreateTestInstallationPath()
        {
            return Path.GetFullPath(Path.Combine (WorkingDirectory, "../" + InstallationDirectoryName));
        }

        public void MoveTo(BaseTestFixture fixture)
        {
            Environment.CurrentDirectory = InstallationPath;
            WorkingDirectory = InstallationPath;
            fixture.WorkingDirectory = InstallationPath;
        }
    }
}

