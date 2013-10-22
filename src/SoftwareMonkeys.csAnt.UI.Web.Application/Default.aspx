<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="SoftwareMonkeys.csAnt.UI.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Default</title>
	<script runat="server">
	public string[] Scripts { get;set; }
	
	public ScriptUtilities Utilities = new ScriptUtilities();

	public void Page_Load(object sender, EventArgs e)
	{
			var projectDirectory = Environment.CurrentDirectory;
			
			// TODO: Clean up
			//Path.GetFullPath(Path.Combine (Environment.CurrentDirectory, "../.."));

			var scriptsDirectory = Path.Combine(projectDirectory, "scripts");

			Scripts = Directory.GetFiles (scriptsDirectory, "*.cs");
	}
	</script>
</head>
<body>
	<form id="form1" runat="server">
		<% foreach (var script in Scripts){ %>
			<a href='<%= Utilities.GetScriptLink(script) %>' ><%= Path.GetFileNameWithoutExtension(script) %><br/>
		<% } %>
	</form>
</body>
</html>
