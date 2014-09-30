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
        //args.CopyTo(path);
        //You can also open the uploaded file's data stream.
        //System.IO.Stream data = args.OpenStream();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Large File Upload in ASP.NET</title>
    <link rel="stylesheet" href="demo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <asp:ScriptManager ID="Scriptmanager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <h2>
                        Large File Upload in ASP.NET</h2>
                    <p>
                        Ajax Uploader allows you to upload large files to a server with the low server memory
                        consumption.</p>
                    <p>
                        Click the following button to upload (No size/type restrictions).</p>
                    <CuteWebUI:Uploader runat="server" ID="Uploader1" InsertText="Upload File" OnFileUploaded="Uploader_FileUploaded">
                    </CuteWebUI:Uploader>
                    <p>
                        Server Trace:
                        <br />
                        <asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
                    </p>
                    <asp:Button ID="ButtonPostBack" Text="This is a PostBack button" runat="server" OnClick="ButtonPostBack_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
