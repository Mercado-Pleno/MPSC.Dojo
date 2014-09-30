<%@ Page Language="vb" %>

<%@ Register Namespace="CuteWebUI" Assembly="CuteWebUI.AjaxUploader" TagPrefix="CuteWebUI" %>
<%@ Register TagPrefix="MagicAjax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Persist uploaded file through postback </title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>

<script runat="server">
		
    Private Sub InsertMsg(ByVal msg As String)
        ListBoxEvents.Items.Insert(0, msg)
        ListBoxEvents.SelectedIndex = 0
    End Sub
		
    Private Sub ButtonPostBack_Click(ByVal sender As Object, ByVal e As EventArgs)
        InsertMsg("You clicked a PostBack Button.")
    End Sub

    Private Sub PersistedFile_FileUploaded(ByVal sender As Object, ByVal args As PersistedFileEventArgs)
        InsertMsg("File is changed! " & args.FileName & ", " & args.FileSize & " bytes.")
        'Copies the uploaded file to a new location.
        'args.CopyTo("c:\\temp\\"& args.FileName)
        'You can also open the uploaded file's data stream.
        'System.IO.Stream data = args.OpenStream()
    End Sub
</script>

<body>
    <form id="Form1" method="post" runat="server">
    <MagicAjax:AjaxPanel runat="server" ID="Ajaxpanel1">
        <div class="content">
            <h2>
                Persist uploaded file through postback
            </h2>
            <p>
                A sample demonstrating how to persist the selected files during page postbacks.
                Ajax Uploader can hold an object temporarily and you can save it anytime you want.
            </p>
            <CuteWebUI:UploadPersistedFile runat="server" ID="PersistedFile1" InsertText="Upload File"
                OnFileChanged="PersistedFile_FileUploaded">
            </CuteWebUI:UploadPersistedFile>
            <p>
                Server Trace:
                <br />
                <asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
            </p>
            <asp:Button ID="ButtonPostBack" Text="This is a PostBack button" runat="server" OnClick="ButtonPostBack_Click" />
        </div>
    </MagicAjax:AjaxPanel>
    </form>
</body>
</html>
