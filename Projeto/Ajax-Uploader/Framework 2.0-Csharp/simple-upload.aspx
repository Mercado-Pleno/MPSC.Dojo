<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    void InsertMsg(string msg)
    {
        ListBoxEvents.Items.Insert(0, msg);
        ListBoxEvents.SelectedIndex = 0;
    }
    void Uploader_FileUploaded(object sender, UploaderEventArgs args)
    {
        Uploader uploader = (Uploader)sender;
        InsertMsg("File uploaded! " + args.FileName + ", " + args.FileSize + " bytes.");

        //Copys the uploaded file to a new location.
        //args.CopyTo("c:\\temp\\"+args.FileName);
        //You can also open the uploaded file's data stream.
        //System.IO.Stream data = args.OpenStream();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple Upload with Progress</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
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
        </div>
    </form>
</body>
</html>
