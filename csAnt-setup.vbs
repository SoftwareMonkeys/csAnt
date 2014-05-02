' Create objects
Dim fso
Set fso = CreateObject("Scripting.FileSystemObject")

Dim shell
Set shell = CreateObject("Wscript.Shell")

Dim path : path = "csAnt-setup.exe"
Dim libDir : libDir="lib"
Dim nugetFile : nugetFile=libDir+"\nuget.exe"
Dim sourcePath : sourcePath="https://www.myget.org/F/softwaremonkeys/"
Dim nugetUrl : nugetUrl="http://nuget.org/nuget.exe"
Dim installDir : installDir = shell.CurrentDirectory

WriteLine ""
WriteLine "Setting up csAnt..."
WriteLine ""
WriteLine "(Please wait. This might take a while, as files need to be downloaded and installed.)"
WriteLine ""

If (NOT fso.FolderExists(MapPath(libDir))) Then
        fso.CreateFolder(MapPath(libDir))
End If

If (NOT fso.FileExists(nugetFile)) Then
        ' Download nuget  

        WriteLine "Getting the nuget.exe file"
        Download nugetUrl, nugetFile, true
        
        WriteLine ""
End If

' Get csAnt setup package
WriteLine "Getting the installer"
WriteLine "(this may take a while as the installer will is being downloaded.... please wait...)"
WriteLine ""
Start(nugetFile + " install csAnt-setup -Source " + sourcePath + " -OutputDirectory lib -NoCache")
WriteLine "Done"
WriteLine ""

' Enter csAnt package dir
Dim setupLibDir : setupLibDir = GetSetupLibDir()
WriteLine "Setup lib dir:"
WriteLine setupLibDir

' Move the setup file back to the root
WriteLine "Moving the installer to the correct location"
WriteLine ""
Dim setupFilePath : setupFilePath = MapPath(setupLibDir + "\csAnt-SetUp.exe")
Dim toSetupFilePath : toSetupFilePath = MapPath(installDir + "\csAnt-SetUp.exe")
WriteLine(toSetupFilePath)
fso.CopyFile setupFilePath, toSetupFilePath

' Run the setup file
WriteLine "Launching the installer..."
WriteLine ""
Start "csAnt-SetUp.exe " + GetArgumentsString()

Function Start(ByVal path)
        Dim objShell
        Set objShell = WScript.CreateObject ("WScript.shell")
        Dim cmd : cmd = path
        Dim objCmdExec
        Set objCmdExec = objShell.exec(cmd)
        WriteLine objCmdExec.StdOut.ReadAll
        Set objShell = Nothing
End Function

Function Download ( ByVal strUrl, ByVal strDestPath, ByVal overwrite )
	Dim intStatusCode, objXMLHTTP, objADOStream
 
	strDestPath = MapPath(strDestPath)
 
	' if the file exists already, and we're not overwriting, quit now
	If Not overwrite And fso.FileExists(strDestPath) Then
		WScript.Echo "Already exists - " & strDestPath
		Download = True
		Exit Function
	End If
 
	WriteLine ""
	WriteLine "Downloading: "
	WriteLine strUrl
	WriteLine "To: "
	WriteLine strDestPath
	WriteLine ""
 
	' Fetch the file
	' need to use ServerXMLHTTP so can set timeouts for downloading large files
	Set objXMLHTTP = CreateObject("MSXML2.ServerXMLHTTP")
	objXMLHTTP.open "GET", strUrl, false
	objXMLHTTP.setTimeouts 1000 * 60 * 1, 1000 * 60 * 1, 1000 * 60 * 1, 1000 * 60 * 7
	objXMLHTTP.send()
 
	intStatusCode = objXMLHTTP.Status
 
	If intStatusCode = 200 Then
		Set objADOStream = CreateObject("ADODB.Stream")
		objADOStream.Open
		objADOStream.Type = 1 'adTypeBinary
		objADOStream.Write objXMLHTTP.ResponseBody
		objADOStream.Position = 0    'Set the stream position to the start
 
		'If the file already exists, delete it.
		'Otherwise, place the file in the specified location
		If fso.FileExists(strDestPath) Then fso.DeleteFile strDestPath
 
		objADOStream.SaveToFile strDestPath
		objADOStream.Close
 
		Set objADOStream = Nothing
	End If
 
	Set objXMLHTTP = Nothing
 
	'WScript.Echo "Status code: " & intStatusCode & VBNewLine
 
	If intStatusCode = 200 Then
		WriteLine "Download successful."
		Download = True
	Else
		WriteLine "Download failed."
		Download = False
	End If
End Function


Function MapPath( sVirtualPath )
	
	MapPath = fso.GetAbsolutePathName(sVirtualPath)
	
End Function


Function WriteLine( v )

	Wscript.stdout.WriteLine(v)
	
End Function

Function GetSetupLibDir ()
        ' TODO: Make this choose the latest folder in cases where multiple are found
        prefix = UCase("csAnt-setup.") 'Must be in upper case!
        Set oFSO = CreateObject("Scripting.FileSystemObject")
        Set oFolder = oFSO.GetFolder(MapPath("lib"))
        For Each oSubFolder In oFolder.SubFolders
                If UCase(Left(oSubFolder.Name, len(prefix))) = prefix _
                    Then GetSetupLibDir = oFolder & "\" & oSubFolder.Name
        Next 
End Function

Function GetArgumentsString()
        Dim argsString : argsString = ""
        For i = 0 to WScript.Arguments.Count - 1
           argsString = argsString + WScript.Arguments(i)
        Next
        GetArgumentsString = Trim(argsString)
End Function

Set fso = nothing
