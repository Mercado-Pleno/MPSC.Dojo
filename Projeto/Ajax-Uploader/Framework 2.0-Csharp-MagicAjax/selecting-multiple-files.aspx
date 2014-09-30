<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
 
    void InsertMsg(string msg)
    {
        ListBoxEvents.Items.Insert(0, msg);
        ListBoxEvents.SelectedIndex = 0;
    }
    void ButtonPostBack_Click(object sender, EventArgs e)
    {
        InsertMsg("You clicked a PostBack Button.");
    }

    void Uploader_FileUploaded(object sender, UploaderEventArgs args)
    {
        InsertMsg("File uploaded! " + args.FileName + ", " + args.FileSize + " bytes.");

        //Copys the uploaded file to a new location.
        //args.CopyTo("c:\\temp\\"+args.FileName);
        //You can also open the uploaded file's data stream.
        //System.IO.Stream data = args.OpenStream();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Selecting multiple files for upload</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <MagicAjax:AjaxPanel runat="server" ID="MagicAjax1">
                <h2>
                    Selecting multiple files for upload</h2>
                <p>
                    Select multiple files in the file browser dialog then upload them at once</p>
                <CuteWebUI:Uploader runat="server" ID="Uploader1" InsertText="Upload Multiple Files (Max 10M)"
                    MultipleFilesUpload="true" OnFileUploaded="Uploader_FileUploaded">
                    <ValidateOption MaxSizeKB="10240" />
                </CuteWebUI:Uploader>
                <p>
                    Server Trace:
                    <br />
                    <asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
                </p>
                <p>
                    <asp:Button ID="ButtonPostBack" Text="This is a PostBack button" runat="server" OnClick="ButtonPostBack_Click" /></p>
            </MagicAjax:AjaxPanel>
        </div>
    </form>
</body>
</html>
