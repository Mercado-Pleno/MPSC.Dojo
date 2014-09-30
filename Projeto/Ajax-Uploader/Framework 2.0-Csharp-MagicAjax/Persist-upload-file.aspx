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

    void PersistedFile_FileUploaded(object sender, PersistedFileEventArgs args)
    {
        InsertMsg("File is changed! " + args.FileName + ", " + args.FileSize + " bytes.");
        //Copys the uploaded file to a new location.
        //args.CopyTo("c:\\temp\\"+args.FileName);
        //You can also open the uploaded file's data stream.
        //System.IO.Stream data = args.OpenStream();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Persist uploaded file through postback</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <MagicAjax:AjaxPanel runat="server" ID="MagicAjax1">
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
                <p>
                    <asp:Button ID="ButtonPostBack" Text="This is a PostBack button" runat="server" OnClick="ButtonPostBack_Click" />
                </p>
            </MagicAjax:AjaxPanel>
        </div>
    </form>
</body>
</html>
