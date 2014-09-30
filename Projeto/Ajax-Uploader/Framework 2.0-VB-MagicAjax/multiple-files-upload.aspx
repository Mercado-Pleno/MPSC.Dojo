<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Private Sub InsertMsg(ByVal msg As String)
        ListBoxEvents.Items.Insert(0, msg)
        ListBoxEvents.SelectedIndex = 0
    End Sub
		
   
    Private Sub Attachments1_AttachmentAdded(ByVal sender As Object, ByVal args As AttachmentItemEventArgs)
        InsertMsg(args.Item.FileName + " has been uploaded.")
    End Sub

    Private Sub ButtonDeleteAll_Click(ByVal sender As Object, ByVal e As EventArgs)
        InsertMsg("Attachments1.DeleteAllAttachments();")
        Attachments1.DeleteAllAttachments()
    End Sub

    Private Sub ButtonTellme_Click(ByVal sender As Object, ByVal e As EventArgs)
        ListBoxEvents.Items.Clear()
        For Each item As AttachmentItem In Attachments1.Items
            InsertMsg(item.FileName & ", " & item.FileSize & " bytes.")
            'Copys the uploaded file to a new location.
            'item.CopyTo("c:\\temp\\"& item.FileName)
            'You can also open the uploaded file's data stream.
            'System.IO.Stream data = item.OpenStream()
        Next
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Uploading multiple files like GMail</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="mod">
            <div class="bd600">
                <div class="content">
                    <MagicAjax:AjaxPanel ID="MagicAjax1" runat="server">
                        <h2>
                            Uploading multiple files like GMail</h2>
                        <p>
                            Google's GMail has a nice way of allowing you to upload multiple files. Rather than
                            showing you 10 file upload boxes at once, the user attaches a file, you can click
                            a button to add another attachment.
                        </p>
                        <br />
                        <CuteWebUI:UploadAttachments InsertText="Upload Multiple files Now" runat="server"
                            ID="Attachments1" MultipleFilesUpload="true" OnAttachmentAdded="Attachments1_AttachmentAdded">
                            <InsertButtonStyle />
                        </CuteWebUI:UploadAttachments>
                        <br />
                        <br />
                        <asp:Button ID="ButtonDeleteAll" runat="server" Text="Delete All" OnClick="ButtonDeleteAll_Click" />&nbsp;&nbsp;
                        <asp:Button ID="ButtonTellme" runat="server" Text="Show Uploaded File Information"
                            OnClick="ButtonTellme_Click" />
                        <br />
                        <br />
                        <div>
                            Server Trace:
                            <br />
                            <asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
                        </div>
                    </MagicAjax:AjaxPanel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
