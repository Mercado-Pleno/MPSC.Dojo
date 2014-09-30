<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

	string filepath;
	System.Xml.XmlDocument doc;
	System.Xml.XmlElement nodeProfile;
	System.Xml.XmlNodeList files;

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		Guid guid = new Guid(Request.QueryString["WizardID"]);
		filepath = Server.MapPath("Data/" + guid.ToString() + ".config");
		if (!System.IO.File.Exists(filepath))
		{
			ShowError("Invalid WizardID");
			return;
		}

		doc = new System.Xml.XmlDocument();
		doc.Load(filepath);
		nodeProfile = (System.Xml.XmlElement)doc.DocumentElement.SelectSingleNode("profile");
		if (nodeProfile == null)
		{
			ShowError("Require Profile");
			return;
		}

		files = doc.DocumentElement.SelectNodes("file");
		if (filepath.Length == 0)
		{
			ShowError("Require Photos");
			return;
		}

		LabelName.Text = HttpUtility.HtmlEncode(nodeProfile.GetAttribute("name"));
		LabelEmail.Text = HttpUtility.HtmlEncode(nodeProfile.GetAttribute("email"));
	}

	void ShowError(string message)
	{
		LabelError.Text = message;
		BtnFinish.Visible = false;
		PanelData.Visible = false;
		PanelError.Visible = true;
	}

	protected void BtnFinish_Click(object sender, EventArgs e)
	{
		//TODO: save the data ...

		//clear the temp data:
		File.Delete(filepath);
		
		Response.Redirect("Complete.Aspx");
	}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Step4</title>
</head>
<body>
	<form id="form1" runat="server">
		<h3>
			Step4 - Confirmation
		</h3>
		<asp:Panel runat="server" ID="PanelError" Visible="false">
			<asp:Label runat="server" ID="LabelError" Font-Bold="true" ForeColor="Red" Font-Size="xx-large"></asp:Label>
		</asp:Panel>
		<asp:Panel runat="server" ID="PanelData">
			<table border="1" cellspacing="0" cellpadding="4" style="border-collapse: collapse;width:100%;">
				<tr style="background-color: SteelBlue; color: White">
					<td style="width:80px;">
					</td>
					<td>
						Profile:
					</td>
				</tr>
				<tr>
					<td>
						Name:
					</td>
					<td>
						<asp:Label runat="server" ID="LabelName" />
					</td>
				</tr>
				<tr>
					<td>
						Email:
					</td>
					<td>
						<asp:Label runat="server" ID="LabelEmail" />
					</td>
				</tr>
				<tr style="background-color: SteelBlue; color: White">
					<td>
					</td>
					<td>
						Files:
					</td>
				</tr>
				<%foreach (System.Xml.XmlElement fileNode in files) %>
				<%{ %>
				<tr>
					<td>
						
					</td>
					<td>
						<img src='DownloadTempPhoto.ashx?guid=<%=fileNode.GetAttribute("guid") %>' border='0' style="width:64px;height:64px;cursor:hand;" onclick="window.open(this.src)"/>
						<%=fileNode.GetAttribute("name") %>
					</td>
				</tr>
				<%} %>
			</table>
		</asp:Panel>
		<div>
			<button id="button_back" onclick="history.back();return false;">
				Back</button>
			<asp:Button runat="server" Text="Finish" ID="BtnFinish" OnClick="BtnFinish_Click" />
		</div>
	</form>
</body>
</html>
