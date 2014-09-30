<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Private Sub InsertMsg(ByVal msg As String)
        ListBoxEvents.Items.Insert(0, msg)
        ListBoxEvents.SelectedIndex = 0
    End Sub


    Private Sub Uploader1_FileUploaded(ByVal sender As Object, ByVal args As UploaderEventArgs)
        InsertMsg("Uploader1 received a new file and passed it to Attachments1.")
            
        Dim stream As System.IO.Stream = args.OpenStream()
        Try
            Attachments1.Upload(args.FileSize, args.FileName, stream)
        Finally
            stream.Close()
        End Try
    End Sub

    Private Sub Attachments1_AttachmentAdded(ByVal sender As Object, ByVal args As AttachmentItemEventArgs)
        InsertMsg("Attachments1 has been added a new item.")
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simulate File Upload</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="mod">
            <div class="bd600">
                <div class="content">
                    <h2>
                        Simulate File Upload</h2>
                    <p>
                        A sample demonstrating how to use .Upload method to simulate a file upload event.
                        The files uploaded by <span style="color: Red">Uploader1</span> will be passed to
                        <span style="color: Red">Attachments1</span>.</p>
                    <p>
                        Click the following button to upload (Maximum file size: 10M).
                    </p>
                    <CuteWebUI:Uploader runat="server" ID="Uploader1" InsertText="Uploader1" OnFileUploaded="Uploader1_FileUploaded">
                        <ValidateOption MaxSizeKB="10240" />
                    </CuteWebUI:Uploader>
                    <br />
                    <p>
                        You can hide the insert button of Attachments1.</p>
                    <CuteWebUI:UploadAttachments runat="server" ID="Attachments1" InsertText="Attachments1"
                        OnAttachmentAdded="Attachments1_AttachmentAdded">
                    </CuteWebUI:UploadAttachments>
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
