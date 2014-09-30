<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Step2</title>

	<script type="text/javascript" src="wizard.js"></script>

	<script type="text/javascript">
function $(id)
{
	return document.getElementById(id);
}
function OnProfileData(obj)
{
	if(obj.profile)
	{
		$("input_name").value=obj.profile.name;
		$("input_email").value=obj.profile.email;
	}
	$("button_save").disabled=false;
}
function LoadProfile()
{
	AsyncLoadWizard(function(xh){
		var obj=ParseWizardResult(xh);
		if(obj==null)return;
		OnProfileData(obj);
	});
}
function SaveProfile()
{
	var name=$("input_name").value;
	var email=$("input_email").value;
	
	if(name.length==0)
	{
		alert("please type your name");
		$("input_name").focus();
		return;
	}
	if(email.length==0)
	{
		alert("please type your email");
		$("input_email").focus();
		return;
	}
	
	$("button_save").disabled=true;
	
	var xh=CreateXMLHttp();
	var guid=GetWizardID();
	var url=WizardAjaxUrl+"?WizardID="+guid+"&Method=SaveProfile&_t="+new Date().getTime();
	xh.open("POST",url,true);
	xh.onreadystatechange=function()
	{
		if(xh.readyState<4)return;
		
		$("button_save").disabled=false;
		
		var res=ParseWizardResult(xh);
		if(res==null)
			return;
		
		location.href="Step3.Aspx?WizardID="+GetWizardID();
	}
	xh.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=utf-8");
	
	var arr=[];
	arr.push("name=");
	arr.push(escape(name));
	arr.push("&");
	arr.push("email=");
	arr.push(escape(email));
	
	xh.send(arr.join(""));
}

	</script>

</head>
<body onload="LoadProfile()">
	<form id="form1" runat="server">
		<h3>
			Step2 - profile
		</h3>
		<div>
			<table border="1" cellspacing="0" cellpadding="4" style="border-collapse: collapse">
				<tr>
					<td>
					</td>
					<td>
						Profile!
					</td>
				</tr>
				<tr>
					<td>
						Name:
					</td>
					<td>
						<input type="text" id="input_name" />
					</td>
				</tr>
				<tr>
					<td>
						Email:
					</td>
					<td>
						<input type="text" id="input_email" />
					</td>
				</tr>
			</table>
			<button id="button_save" disabled="disabled" onclick="SaveProfile();return false;">
				Save profile , and continue
			</button>
		</div>
	</form>
</body>
</html>
