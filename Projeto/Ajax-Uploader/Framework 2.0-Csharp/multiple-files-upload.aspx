<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    void InsertMsg(string msg)
    {
        ListBoxEvents.Items.Insert(0, msg);
        ListBoxEvents.SelectedIndex = 0;
    }

    void Attachments1_AttachmentAdded(object sender, AttachmentItemEventArgs args)
    {
        InsertMsg(args.Item.FileName + " has been uploaded.");
    }

    void ButtonDeleteAll_Click(object sender, EventArgs e)
    {
        InsertMsg("Attachments1.DeleteAllAttachments();");
        Attachments1.DeleteAllAttachments();
    }
    void ButtonTellme_Click(object sender, EventArgs e)
    {
        ListBoxEvents.Items.Clear();
        foreach (AttachmentItem item in Attachments1.Items)
        {
            InsertMsg(item.FileName + ", " + item.FileSize + " bytes.");
            //Copies the uploaded file to a new location.
            //item.CopyTo("c:\\temp\\"+item.FileName);
            //You can also open the uploaded file's data stream.
            //System.IO.Stream data = item.OpenStream();
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Uploading multiple files like GMail</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
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
        </div>
    </form>
</body>
</html>
