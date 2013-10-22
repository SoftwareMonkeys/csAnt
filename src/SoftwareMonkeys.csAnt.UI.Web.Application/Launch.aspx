<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="SoftwareMonkeys.csAnt.UI.Web" %>
<%@ Import Namespace="SoftwareMonkeys.csAnt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Launch</title>
	<script runat="server">
	public WebLauncherScript LauncherScript = new WebLauncherScript();
	
	public string OriginalDirectory;
	
	public void Page_Load(object sender, EventArgs e)
	{
		LauncherScript = CreateLauncherScript();
	
		var scriptName = Request.QueryString["Script"];
		
		if (HasArguments(scriptName))
			DisplayArgumentsForm(scriptName);
		else
			Launch(scriptName);
	}
	
	public WebLauncherScript CreateLauncherScript()
	{
		var script = new WebLauncherScript();
		
		OriginalDirectory = script.CurrentDirectory;
		
		
		return script;
	}
	
	public bool HasArguments(string scriptName)
	{
		var script = LauncherScript.GetScript(scriptName);
		
		Console.WriteLine(script.GetType().GetCustomAttributes(typeof(ExpectedArgumentAttribute), true).Length.ToString());
		
		return true;
	}
	
	public void DisplayArgumentsForm(string scriptName)
	{
		
	}
	
	public void Launch(string scriptName)
	{
		LauncherScript.ExecuteScript(scriptName);
		
		foreach (string line in LauncherScript.Console.Output.Split('\n'))
		{
			OutputHolder.Controls.Add(new LiteralControl(HttpUtility.HtmlEncode(line) + "<br/>"));
		}
		
		LauncherScript.CurrentDirectory = OriginalDirectory;
	}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<p>Output:</p>
	<div style="padding: 3px; width: 100%; height: 80% auto; border: solid 1px navy; font-family: courier;">
	<asp:Placeholder runat="server" id="OutputHolder">
	</asp:Placeholder>
	</div>
	</form>
</body>
</html>
