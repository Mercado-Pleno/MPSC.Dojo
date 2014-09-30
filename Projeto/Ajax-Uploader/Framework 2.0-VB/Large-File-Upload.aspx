<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Private Sub InsertMsg(ByVal msg As String)
        ListBoxEvents.Items.Insert(0, msg)
        ListBoxEvents.SelectedIndex = 0
    End Sub
	
    Private Sub Uploader_FileUploaded(ByVal sender As Object, ByVal args As UploaderEventArgs)
        InsertMsg("File uploaded! " & args.FileName & ", " & args.FileSize & " bytes.")
        'Copys the uploaded file to a new location.
        'args.CopyTo("c:\\temp\\"& args.FileName)
        'You can also open the uploaded file's data stream.
        'System.IO.Stream data = args.OpenStream()
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Large File Upload </title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="mod">
            <div class="bd600">
                <div class="content">
                    <h2>
                        Large File Upload in ASP.NET</h2>
                    <p>
                        Ajax Uploader allows you to upload large files to a server with the low server memory
                        consumption.</p>
                    <p>
                        Click the following button to upload (No size/type restrictions).
                    </p>
                    <CuteWebUI:Uploader runat="server" ID="Uploader1" InsertText="Upload File" OnFileUploaded="Uploader_FileUploaded">
                    </CuteWebUI:Uploader>
                    <br />
                    <br />
                    <div>
                        Server Trace:
                        <br />
                        <asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
