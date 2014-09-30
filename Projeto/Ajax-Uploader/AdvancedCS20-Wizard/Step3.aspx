<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Step3</title>

	<script type="text/javascript" src="wizard.js"></script>

	<script type="text/javascript">
function $(id)
{
	return document.getElementById(id);
}
function OnProfileData(obj)
{
	var table=$("table_files");
	while(table.rows.length>1)table.deleteRow(1);
	
	$("button_continue").disabled=(obj.files.length==0);
	
	for(var i=0;i<obj.files.length;i++)
	{
		var file=obj.files[i];
		var row=table.insertRow(-1);
		row.insertCell(-1).innerHTML=String(i+1);
		row.insertCell(-1).innerHTML=file.name;
		row.insertCell(-1).innerHTML=file.size;
		row.insertCell(-1).innerHTML=file.guid;
		
		var img=document.createElement("IMG");
		img.src="DownloadTempPhoto.ashx?guid="+file.guid;
		img.border=0;
		img.style.width="64px";
		img.style.height="64px";
		img.style.cursor="hand";
		img.onclick=new Function("","window.open(this.src)");
		
		//row.insertCell(-1).appendChild(img)
		
		
		var link=document.createElement("A");
		link.href="#";
		link.onclick=new Function("","DoDeleteFileLink(this)");
		link.setAttribute("fileguid",file.guid);
		link.setAttribute("filename",file.name);
		link.innerHTML="remove";

		row.insertCell(-1).appendChild(link)
	}
}
function LoadProfile()
{
	AsyncLoadWizard(function(xh){
		var obj=ParseWizardResult(xh);
		if(obj==null)return;
		OnProfileData(obj);
	});
}
function DoDeleteFileLink(link)
{
	var fileguid=link.getAttribute("fileguid");
	var filename=link.getAttribute("filename");
	
	if( ! confirm("Delete '"+filename+"' ?") )
		return;
	
	var xh=CreateXMLHttp();
	var guid=GetWizardID();
	var url=WizardAjaxUrl+"?WizardID="+guid+"&Method=DeleteFile&_t="+new Date().getTime();
	xh.open("POST",url,false);
	xh.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=utf-8");
	xh.send("fileguid="+fileguid);

	var res=ParseWizardResult(xh);
	if(res==null)
		return;
	
	OnProfileData(res);
}
function CuteWebUI_AjaxUploader_OnPostback()
{
	var guidlist=$("myuploader").value;
	
	var xh=CreateXMLHttp();
	var guid=GetWizardID();
	var url=WizardAjaxUrl+"?WizardID="+guid+"&Method=AddFiles&_t="+new Date().getTime();
	xh.open("POST",url,true);
	xh.onreadystatechange=function()
	{
		if(xh.readyState<4)return;
		
		$("myuploader").reset();
		
		var res=ParseWizardResult(xh);
		if(res==null)
			return;
		
		OnProfileData(res);
	}
	xh.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=utf-8");
	xh.send("files="+guidlist);
}

function DoContinue()
{
	location.href="Step4.Aspx?WizardID="+GetWizardID();
}
	</script>

</head>
<body onload="LoadProfile()">
	<form id="form1" runat="server">
		<h3>
			Step3 - Attachments
		</h3>
		<div>
			<%
				CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(Context);
				uploader.Name = "myuploader";
				uploader.InsertText = "Add photos";
				uploader.MultipleFilesUpload = true;
				uploader.UploadUrl = "UploadHandler.ashx";
				uploader.AllowedFileExtensions = "*.png,*.jpg,*.gif,*.bmp";
			%>
			<%=uploader.Render() %>
		</div>
		<div style="min-height: 240px;">
			<table id="table_files" border="1" cellspacing="0" cellpadding="4" style="border-collapse: collapse;
				width: 100%;">
				<tr style="background-color: SteelBlue; color: White;">
					<td>
						Index
					</td>
					<td>
						Name
					</td>
					<td>
						Size
					</td>
					<td>
						Guid
					</td>
					<td>
						&nbsp;
					</td>
					<td>
						&nbsp;
					</td>
				</tr>
			</table>
		</div>
		<div>
			<button id="button_back" onclick="history.back();return false;">
				Back
			</button>
			<button id="button_continue" disabled="disabled" onclick="DoContinue();return false;">
				Continue
			</button>
		</div>
	</form>
</body>
</html>
