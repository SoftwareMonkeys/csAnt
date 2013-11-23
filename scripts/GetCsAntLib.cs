//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/FileNodes/bin/Release/SoftwareMonkeys.FileNodes.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.FileNodes;

class GetCsAntLibScript : BaseScript
{
	public static void Main(string[] args)
	{
		new GetCsAntLibScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        string name = "csAnt";
	        
	        var arguments = new Arguments(args);
	        
	        var force = arguments.Contains("f");
	        
	        CreateNode();
	        
	        var found = false;

                if (LibExists(name) && !force)
                {
                        Console.WriteLine("csAnt libraries already exist. Skipping.");
                }
                else
                {
                        if (LibExists(name) && force)
                                Console.WriteLine("csAnt libraries already exist. Overwriting.");
                
	                // Try locally
                        found = GetLocally(name);
                                
                        // Try from the web
                        if (!found)
                                found = GetFromWeb(name);
                }
                
                
		return !IsError;
	}
	
	public void CreateNode()
	{
	        var path = CurrentDirectory
	                + Path.DirectorySeparatorChar
	                + "lib"
	                + Path.DirectorySeparatorChar
	                + "csAnt"
	                + Path.DirectorySeparatorChar
	                + "csAnt.node";
	
	        if (!File.Exists(path))
	        {
	                Console.WriteLine("Creating node:");
	                Console.WriteLine(path);
	        
	                var node = new FileNode(
	                        path,
	                        new FileNodeSaver()
	                );

	                node.Name = "csAnt";	        
	                node.Properties.Add("ImportScript", "GetCsAntLib");
	                
	                node.Save();
	                
	                if (!CurrentNode.Nodes["Libraries"].Nodes.ContainsKey("csAnt"))
                	        CurrentNode.Nodes["Libraries"].Nodes.Add("csAnt", node);
        	}
        	else
        	{
	                Console.WriteLine("Node already found:");
	                Console.WriteLine(path);
	        }
	}
	
	public bool GetLocally(string name)
	{
	        var found = false;
	
	        var groupLibDir = Path.GetFullPath(
	                CurrentDirectory
	                + "/../lib"
	        );
	        
	        var groupCsAntLibZip = Path.GetFullPath(
	                groupLibDir
	                + "/csAnt/csAnt.zip"
	        );
	
	        if (File.Exists(groupCsAntLibZip))
	        {
	                Console.WriteLine("Found csAnt lib zip file locally:");
	                Console.WriteLine(groupCsAntLibZip);
	        
	                var toFile = GetCsAntLibZip();
	                File.Copy(groupCsAntLibZip, toFile, true);
	                
	                found = true;
	        }
	        else
	                Console.WriteLine("csAnt lib zip file not found in group libraries direcory.");
	        
	        if (!found)
	        {
                        var projectsLibDir = Path.GetFullPath(
	                        CurrentDirectory
	                        + "/../../lib"
	                );
	                
	                var projectsCsAntLibZip = Path.GetFullPath(
	                        projectsLibDir
	                        + "/csAnt/csAnt.zip"
	                );
	
	                if (File.Exists(projectsCsAntLibZip))
	                {
	                        Console.WriteLine("Found csAnt lib zip file locally:");
	                        Console.WriteLine(projectsCsAntLibZip);
	                
	                        var toFile = GetCsAntLibZip();
	                        File.Copy(projectsCsAntLibZip, toFile, true);
	                        
	                        found = true;
	                }
	        }
	        
	        if (!found)
	                Console.WriteLine("csAnt lib zip file not found in general libraries direcory.");
	        
	        return found;
	}
	
	public bool GetFromWeb(string name)
	{
	        string url = GetcsAntUrl();

	        Console.WriteLine("Retrieving csAnt files from:");
	        Console.WriteLine(url);
	        
                var dir = CurrentDirectory
                        + Path.DirectorySeparatorChar
                        + "libs"
                        + Path.DirectorySeparatorChar
                        + name;

                DownloadAndUnzip(url, dir, true);
                
                return !IsError;
	}
	
	public string GetCsAntLibZip()
	{
	        return CurrentDirectory
	                + Path.DirectorySeparatorChar
	                + "lib"
	                + Path.DirectorySeparatorChar
	                + "csAnt"
	                + Path.DirectorySeparatorChar
	                + "csAnt.zip";
	}
	
	public string GetcsAntUrl()
	{
		var url = "https://code.google.com/p/csant/downloads/list";

		var xpath = "//table[@id='resultstable']/tr/td[3]";

		var prefix = "https://csant.googlecode.com/files/";

		var data = ScrapeXPathArray(
			url,
			xpath
		);

		foreach (string item in data)
		{
			if (item.IndexOf("csAnt-project-release-") == 0)
			{
				Console.WriteLine("csAnt URL: " + prefix + item);

				return prefix + item;
			}
		}

		return String.Empty;
	}

}
