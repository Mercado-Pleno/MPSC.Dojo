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

    void SubmitButton_Click(object sender, EventArgs e)
    {
        InsertMsg("You clicked the Submit Button.");
        InsertMsg("You have uploaded " + uploadcount + "/" + Uploader1.Items.Count + " files.");
    }

    int uploadcount = 0;

    void Uploader_FileUploaded(object sender, UploaderEventArgs args)
    {
        uploadcount++;

        InsertMsg("File uploaded! " + args.FileName + ", " + args.FileSize + " bytes.");

        //Copys the uploaded file to a new location.
        //args.CopyTo(path);
        //You can also open the uploaded file's data stream.
        //System.IO.Stream data = args.OpenStream();
    }

    protected override void OnPreRender(EventArgs e)
    {
        SubmitButton.Attributes["itemcount"] = Uploader1.Items.Count.ToString();

        base.OnPreRender(e);
    }
	
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Start uploading manually</title>
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
                        Start uploading manually</h2>
                    <p>
                        This sample demonstrates how to start uploading manually after file selection vs
                        automatically.</p>
                    <CuteWebUI:UploadAttachments runat="server" ManualStartUpload="true" ID="Uploader1"
                        InsertText="Browse Files (Max 1M)" OnFileUploaded="Uploader_FileUploaded">
                        <ValidateOption MaxSizeKB="1024" />
                    </CuteWebUI:UploadAttachments>
                    <br />
                    <br />
                    <asp:Button runat="server" ID="SubmitButton" OnClientClick="return submitbutton_click()"
                        Text="Submit" OnClick="SubmitButton_Click" />
                    <br />
                    <br />
                    <div>
                        <asp:ListBox runat="server" ID="ListBoxEvents" Width="400"></asp:ListBox>
                    </div>
                    <br />
                    <br />
                    <asp:Button ID="ButtonPostBack" Text="This is a PostBack button" runat="server" OnClick="ButtonPostBack_Click" />

                    <script type="text/javascript">
	
	function submitbutton_click()
	{
		var submitbutton=document.getElementById('<%=SubmitButton.ClientID %>');
		var uploadobj=document.getElementById('<%=Uploader1.ClientID %>');
		if(!window.filesuploaded)
		{
			if(uploadobj.getqueuecount()>0)
			{
				uploadobj.startupload();
			}
			else
			{
				var uploadedcount=parseInt(submitbutton.getAttribute("itemcount"))||0;
				if(uploadedcount>0)
				{
					return true;
				}
				alert("Please browse files for upload");
			}
			return false;
		}
		window.filesuploaded=false;
		return true;
	}
	function CuteWebUI_AjaxUploader_OnPostback()
	{
		window.filesuploaded=true;
		var submitbutton=document.getElementById('<%=SubmitButton.ClientID %>');
		submitbutton.click();
		return false;
	}
                    </script>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
