<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Private Sub InsertMsg(ByVal msg As String)
        ListBoxEvents.Items.Insert(0, msg)
        ListBoxEvents.SelectedIndex = 0
    End Sub

   
    Private Sub ButtonPostBack_Click(ByVal sender As Object, ByVal e As EventArgs)
        InsertMsg("You clicked a PostBack Button.")
    End Sub

    Private Sub Uploader_FileUploaded(ByVal sender As Object, ByVal args As UploaderEventArgs)
        InsertMsg("File uploaded! " & args.FileName & ", " + args.FileSize.ToString() & " bytes.")
        'Copys the uploaded file to a new location. 
        'args.CopyTo("c:\\temp\\" & args.FileName)
        'You can also open the uploaded file's data stream. 
        'System.IO.Stream data = args.OpenStream(); 
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple Upload with Progress</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <h2>
                        Simple Upload with Progress</h2>
                    <p>
                        A basic sample demonstrating the use of the Upload control.</p>
                    <CuteWebUI:Uploader runat="server" ID="Uploader1" InsertText="Upload File (Max 10M)"
                        OnFileUploaded="Uploader_FileUploaded">
                        <ValidateOption MaxSizeKB="10240" />
                    </CuteWebUI:Uploader>
                    <br />
                    <br />
                    <div>
                        Server Trace:
                        <br />
                        <asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
                    </div>
                    <br />
                    <br />
                    <asp:Button ID="ButtonPostBack" Text="This is a PostBack button" runat="server" OnClick="ButtonPostBack_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
