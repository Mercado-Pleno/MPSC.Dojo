<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    void InsertMsg(string msg)
    {
        ListBoxEvents.Items.Insert(0, msg);
        ListBoxEvents.SelectedIndex = 0;
    }

    protected override void OnInit(EventArgs e)
    {

        Attachments1.InsertButton.Style["display"] = "none";
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

    void Uploader_FileUploaded(object sender, UploaderEventArgs args)
    {
        if (GetVisibleItemCount() >= 3)
            return;
        using (System.IO.Stream stream = args.OpenStream())
        {
            Attachments1.Upload(args.FileSize, "ChangeName-" + args.FileName, stream);
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (GetVisibleItemCount() >= 3)
        {
            Uploader1.InsertButton.Enabled = false;
        }
        else
        {
            Uploader1.InsertButton.Enabled = true;
        }
    }
    int GetVisibleItemCount()
    {
        int count = 0;
        foreach (AttachmentItem item in Attachments1.Items)
        {
            if (item.Visible)
                count++;
        }
        return count;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Uploading multiple files</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <h2>
                Uploading multiple files (Limit the maximum allowed number of files to be uploaded)</h2>
            <p>
                This example shows you how to limit the maximum allowed number of files to be uploaded.
                In the following example, you can only upload 3 files.</p>
            <br />
            <fieldset style="height: 130px">
                <legend>
                    <CuteWebUI:Uploader runat="server" ID="Uploader1" InsertText="Upload Multiple files Now"
                        MultipleFilesUpload="true" OnFileUploaded="Uploader_FileUploaded">
                    </CuteWebUI:Uploader>
                </legend>
                <div>
                    <CuteWebUI:UploadAttachments runat="server" ID="Attachments1" OnAttachmentAdded="Attachments1_AttachmentAdded">
                    </CuteWebUI:UploadAttachments>
                </div>
            </fieldset>
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
