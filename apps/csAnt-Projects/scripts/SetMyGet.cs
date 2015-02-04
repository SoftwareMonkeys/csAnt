using System;
using System.IO;
using System.Net;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class SetMyGet : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new SetMyGet().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	    var apiKey = Arguments["k", "key", "ApiKey"];
	    
	    var username = Arguments["u", "user", "username"];
	    
	    var password = Arguments["p", "pass", "password"];
	    
	    EnsureSecurityNode();
	    
	    EnsureMyGetNode();
	    
	    var myGetNode = CurrentNode.Nodes["Security"].Nodes["MyGet"];
	    
	    if (!String.IsNullOrEmpty(apiKey))
	    {
    	    Console.WriteLine("ApiKey: " + apiKey);
    	    myGetNode.Properties["ApiKey"] = apiKey;
	    }
	    
	    if (!String.IsNullOrEmpty(username))
	    {
    	    Console.WriteLine("Username: " + username);
	        myGetNode.Properties["Username"] = username;
	    }
	    
	    if (!String.IsNullOrEmpty(password))
	    {
	    	Console.WriteLine("Password: [hidden]");
	        myGetNode.Properties["Password"] = password; 
	    }
	    
	    myGetNode.Save();
	        
		return !IsError;
	}
	
	public void EnsureSecurityNode()
	{
	    if (!CurrentNode.Nodes.ContainsKey("Security"))
	    {
	        ExecuteScript("CreateSecurityNode");
	    }
	}
	
	public void EnsureMyGetNode()
	{
	    if (!CurrentNode.Nodes["Security"].Nodes.ContainsKey("MyGet"))
	    {
	        var node = new FileNode(CurrentDirectory + "/_security/MyGet/MyGet.node", new FileNodeSaver());
	        
	        node.Name = "MyGet";
	        
	        node.Save();
	        
	        CurrentNode.Nodes["Security"].Nodes.Add("MyGet", node);
	    }
    }
}
