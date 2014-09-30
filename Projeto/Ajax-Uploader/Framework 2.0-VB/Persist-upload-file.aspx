<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
Private Sub InsertMsg(ByVal msg As String) 
    		ListBoxEvents.Items.Insert(0, msg) 
			ListBoxEvents.SelectedIndex = 0 
		End Sub 		
		
        Protected Overloads Overrides Sub OnInit(ByVal e As EventArgs) 
			MyBase.OnInit(e) 
        SampleUtil.SetPageCache()
            AddHandler PersistedFile1.FileChanged, AddressOf PersistedFile_FileUploaded 
			AddHandler ButtonPostBack.Click, AddressOf ButtonPostBack_Click 
		End Sub 
		Private Sub ButtonPostBack_Click(ByVal sender As Object, ByVal e As EventArgs) 
			InsertMsg("You clicked a PostBack Button.") 
		End Sub 

		Private Sub PersistedFile_FileUploaded(ByVal sender As Object, ByVal args As PersistedFileEventArgs) 
			InsertMsg("File is changed! " & args.FileName & ", " & args.FileSize & " bytes.") 
            'Copys the uploaded file to a new location.
        'args.CopyTo("c:\\temp\\"& args.FileName)
            'You can also open the uploaded file's data stream.
            'System.IO.Stream data = args.OpenStream()
		End Sub 
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Persist uploaded file through postback</title>
        
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
  
        <div class="mod">
            <div class="bd600">
                <div class="content">
                <h2>Persist uploaded file through postback </h2>
	<p> A sample demonstrating how to persist the selected files during page postbacks. Ajax Uploader can hold an object temporarily and you can save it anytime you want. </p>
	<p>Click the following button to upload (Maximum file size: 10M). 
	</p>
	<CuteWebUI:UploadPersistedFile runat="server" ID="PersistedFile1" InsertText="Upload File">
		<VALIDATEOPTION MaxSizeKB="10240" />
	</CuteWebUI:UploadPersistedFile>
	
	<br /><br />
	<div>
	    Server Trace:
	    <br />
		<asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
	</div>
	 <br />
	 <br />
	<asp:Button ID="ButtonPostBack" Text="This is a PostBack button" runat="server"/>	
                </div>
            </div>
        </div>
    </form>
</body>
</html>
