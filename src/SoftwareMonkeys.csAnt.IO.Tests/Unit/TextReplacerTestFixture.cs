using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;


namespace SoftwareMonkeys.csAnt.IO.Tests
{
    [TestFixture]
    public class TextReplacerTestFixture : BaseIOUnitTestFixture
    {
        [Test]
        public void ReplaceIn()
        {
            var testFileName = Path.Combine(CurrentDirectory, "TestFolder/HelloWorld/TestFile.txt");

            // Copy binary files over to test that they're skipped
            new FileCopier(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                WorkingDirectory
                ).Copy("*.dll");

            var testFileContent = "Hello World";

            var searchText = "World";

            var replacementText = "Universe";

            DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(testFileName));

            File.WriteAllText(testFileName, testFileContent);

            var replacer = new TextReplacer();
            replacer.ReplaceIn(
                Path.Combine(CurrentDirectory, "TestFolder"),
                "**",
                searchText,
                replacementText,
                true
            );

            var newFileName = Path.Combine(CurrentDirectory, "TestFolder/HelloUniverse/TestFile.txt");

            Assert.IsTrue(File.Exists(newFileName), "File name wasnt changed.");

            var newText = File.ReadAllText(newFileName);

            Assert.IsTrue(
                newText.Contains(replacementText),
                "The replacement text wasn't found."
                );
        }

        [Test]
        public void ReplaceIn_DontCommit()
        {
            // Copy binary files over to test that they're skipped
            new FileCopier(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                WorkingDirectory
                ).Copy("*.dll");

            var testFileName = Path.Combine(CurrentDirectory, "TestFolder/HelloWorld/TestFile.txt");

            var testFileContent = "Hello World";

            var searchText = "World";

            var replacementText = "Universe";

            DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(testFileName));

            File.WriteAllText(testFileName, testFileContent);

            var replacer = new TextReplacer();
            replacer.ReplaceIn(Path.Combine(CurrentDirectory, "TestFolder"), "**", searchText, replacementText, false);

            var newText = File.ReadAllText(testFileName);

            Assert.IsTrue(File.Exists(testFileName), "File name must have been changed when it shouldn't have.");

            Assert.IsFalse(
                newText.Contains(replacementText),
                "The replacement text was found when it shouldn't have been."
                );
        }
    }
}

