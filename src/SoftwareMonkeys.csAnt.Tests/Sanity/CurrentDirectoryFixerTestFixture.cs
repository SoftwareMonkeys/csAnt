﻿using System;
using System.IO;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests.Sanity
{
	/// <summary>
	/// 
	/// </summary>
	public class CurrentDirectoryFixerTestFixture : BaseSanityTestFixture
	{
		public CurrentDirectoryFixerTestFixture()
		{
		}
		
		[Test]
		public void Test_Fix_AnyCPU()
		{			
			var startingDir = "";
			if (IsLinux)
				startingDir = "/home/user/projects/group/project/bin/Release";
			else
				startingDir = @"c:\projects\group\project\bin\Release";
			
			var fixedDir = new CurrentDirectoryFixer().Fix(startingDir);
			
			var expectedDir = startingDir.Replace(
					Path.DirectorySeparatorChar
					+ "bin"
					+ Path.DirectorySeparatorChar
					+ "Release",
					""
				);
			
			Assert.AreEqual(expectedDir, fixedDir, "Invalid path.");
		}
		
		[Test]
		public void Test_Fix_x86()
		{			
			var startingDir = "";
			
			if (IsLinux)
				startingDir = "/home/user/projects/group/project/bin/Release/x86";
			else
				startingDir = @"c:\projects\group\project\bin\Release\x86";
			
			
			var fixedDir = new CurrentDirectoryFixer().Fix(startingDir);
			
			var expectedDir = startingDir.Replace(
					Path.DirectorySeparatorChar
					+ "bin"
					+ Path.DirectorySeparatorChar
					+ "Release"
					+ Path.DirectorySeparatorChar
					+ "x86",
					""
				);
			
			Assert.AreEqual(expectedDir, fixedDir, "Invalid path.");
		}
	}
}
