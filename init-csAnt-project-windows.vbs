
' Set variables

' Libs
Dim libsDir : libsDir = "lib"

' Python
Dim portablePythonUrl : portablePythonUrl = "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qMjE0aWhqaWxTOVk"
Dim portablePythonDir : portablePythonDir = libsDir & "/PortablePython/"
Dim portablePythonZipFile : portablePythonZipFile = portablePythonDir & "/PortablePython.zip"
Dim portablePythonExe : portablePythonExe = portablePythonDir & "/App/python.exe"

' 7-zip
Dim sevenZipUrl : sevenZipUrl = "http://drive.google.com/uc?export=download&id=0B_8QvsLqRy5qOVV0Vmd6T2d6b2c"
Dim sevenZipDir : sevenZipDir = libsDir & "/7Zip/"
Dim sevenZipFile : sevenZipFile = sevenZipDir + "/7z.exe"

' Scripts
Dim scriptsDir : scriptsDir = "scripts"

' Initialize script
Dim initScriptUrl : initScriptUrl = "https://csant.googlecode.com/git/scripts/Initialize/Initialize.py"
Dim initScriptsDir : initScriptsDir = scriptsDir & "/Initialize"
Dim initScriptFile : initScriptFile = initScriptsDir & "/Initialize.py"

' Create objects
Dim fso
Set fso = CreateObject("Scripting.FileSystemObject")

WriteLine ""
WriteLine "Current directory:"
WriteLine fso.GetAbsolutePathName(".")
WriteLine ""

WriteLine ""
WriteLine("Portable python path:")
WriteLine(portablePythonDir)
WriteLine ""

' Create the libs directory if necessary
If Not fso.FolderExists(MapPath(libsDir)) Then
	CreateDirectory MapPath(libsDir)
End If

' Download 7-zip command line version
CreateDirectory sevenZipDir
Download sevenZipUrl, sevenZipFile, false

' Download portable python for windows
CreateDirectory portablePythonDir
Download portablePythonUrl, portablePythonZipFile, false

' Unzip portable python
Unzip portablePythonZipFile, portablePythonDir, "PortablePython"

' Create the scripts directory if necessary
If Not fso.FolderExists(MapPath(scriptsDir)) Then
	CreateDirectory MapPath(scriptsDir)
End If

' Create the initialize scripts directory if necessary
If Not fso.FolderExists(MapPath(initScriptsDir)) Then
	CreateDirectory MapPath(initScriptsDir)
End If

' Download the Initialize.py file
Download initScriptUrl, MapPath(initScriptFile), true

' Launch the Initialize.py file
StartPython MapPath(initScriptFile)


' ----------
' Functions
' ----------

Function StartPython( file )

	StartProcess MapPath(portablePythonExe) & " " & file

End Function

Function StartProcess( command )

	WriteLine ""
	WriteLine "Starting process:"
	WriteLine command
	WriteLine ""

	Dim tmpFile : tmpFile = MapPath("log-" + GetTimeStamp & ".txt")
	
	Set oShell = WScript.CreateObject ("WScript.Shell")
	
	command = "cmd /c " & command & " > " & tmpFile
	
	oShell.run command, 0, true 
	
	Set oShell = Nothing
End Function

Function ReadFile( file )

	Dim output

	Set objFile = fso.OpenTextFile(file, 1)

	output = objFile.ReadAll

	objFile.Close

	return = output

End Function

Function Unzip( file, destination, subDir )

	WriteLine ""
	WriteLine "Unzipping:"
	WriteLine file
	WriteLine "To:"
	WriteLine destination
	WriteLine ""
	
	StartProcess MapPath(sevenZipFile) & " x " & MapPath(file) & " -o" + MapPath(destination)
	
	fullSubDir = MapPath(destination & "/" & subDir)
	
	MoveDirectory fullSubDir, MapPath(destination)

	' Delete the now empty sub folder
	fso.DeleteFolder fullSubDir
	
End Function

Function MoveDirectory(fromDir, toDir)
	WriteLine("Moving directory from:")
	WriteLine(fromDir)
	WriteLine("To:")
	WriteLine(toDir)

	fso.MoveFile fromDir & "/**", toDir
	
	fso.MoveFolder fromDir & "/**", toDir
End Function

Function CreateDirectory ( name )

	If InStr(name, ":") = 0 Then
		name = MapPath(name)
	End If

	If NOT fso.FolderExists(name) Then
	
		WriteLine ""
		WriteLine "Creating directory:"
		WriteLine name
		WriteLine ""
	
		' Create a new folder
		fso.CreateFolder name
	End If

End Function

Function MapPath( sVirtualPath )
	
	MapPath = fso.GetAbsolutePathName(sVirtualPath)
	
End Function

Function WriteLine( v )

	'Set fso = CreateObject ("Scripting.FileSystemObject")
	'Set stdout = fso.GetStandardStream (1)
	Wscript.stdout.WriteLine(v)
	
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
	WriteLine "Please wait (this might take a while depending on the size of the file)..."
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

Function GetTimeStamp
  Dim CurrTime
  CurrTime = Now()

  GetTimeStamp = CStr(Year(CurrTime)) & "-" _
    & Month(CurrTime) & "-" _
    & Day(CurrTime) & "-" _
    & Hour(CurrTime) & "-" _
    & Minute(CurrTime) & "-" _
    & Second(CurrTime)
End Function

Set fso = nothing