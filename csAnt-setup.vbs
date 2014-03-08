' Create objects
Dim fso
Set fso = CreateObject("Scripting.FileSystemObject")

Dim url : url = "https://csant.googlecode.com/files/csAnt-SetUp--0-2-0-1600-%5B2014-3-7--17-51-58%5D.exe"

Dim path : path = "csAnt-setup.exe"

Download url, path, true

Start(path)

Function Start(ByVal path)
        Dim objShell
        Set objShell = WScript.CreateObject ("WScript.shell")
        Dim cmd : cmd = path
        objShell.run cmd
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
	Set fso = Nothing
 
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

Set fso = nothing