<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

	protected void BtnAgree_Click(object sender, EventArgs e)
	{
		Guid guid = new Guid(Request.QueryString["WizardID"]);
		string filepath = Server.MapPath("Data/" + guid.ToString() + ".config");
		if (!System.IO.File.Exists(filepath))
		{
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml("<wizard />");
			doc.DocumentElement.SetAttribute("userid", "SetUserIdHereForSecurity");
			doc.Save(filepath);
		}
		Response.Redirect("Step2.Aspx?WizardID=" + guid);
	}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Step1</title>

	<script type="text/javascript" src="wizard.js"></script>

</head>
<body>
	<form id="form1" runat="server">
		<h3>
			Step1 - Agreement
		</h3>
		<div>
			List of agreement
			<ul>
				<li>1111111111</li>
				<li>2222222222</li>
				<li>3333333333</li>
				<li>4444444444</li>
				<li>5555555555</li>
				<li>6666666666</li>
				<li>7777777777</li>
			</ul>
			<asp:Button ID="BtnAgree" runat="server" Text="I accept this agreement , continue" OnClick="BtnAgree_Click" />
		</div>
	</form>
</body>
</html>
