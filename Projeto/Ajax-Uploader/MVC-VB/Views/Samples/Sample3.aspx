<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sample 3 - Accessing uploaded files using Ajax
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		var handlerurl='<%=Url.Action("Sample3Ajax") %>'
	</script>
	<script type="text/javascript">
		function CuteWebUI_AjaxUploader_OnPostback()
		{

			var uploader = document.getElementById("myuploader");
			var guidlist = uploader.value;

			//Send Request
			var xh;
			if (window.XMLHttpRequest)
				xh = new window.XMLHttpRequest();
			else
				xh = new ActiveXObject("Microsoft.XMLHTTP");
			xh.open("POST", handlerurl, false, null, null);
			xh.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
			xh.send("guidlist=" + guidlist);

			//call uploader to clear the client state
			uploader.reset();

			if (xh.status != 200)
			{
				alert("http error " + xh.status);
				setTimeout(function() { document.write(xh.responseText); }, 10);
				return;
			}

			var filelist = document.getElementById("filelist");

			var list = eval(xh.responseText); //get JSON objects
			//Process Result:
			for (var i = 0; i < list.length; i++)
			{
				var item = list[i];
				var msg;
				if (item.Error != null)
				{
					msg = "Error " + item.FileGuid + " - " + item.Error;
				}
				else
				{
					msg = "Processed : " + list[i].FileName;
				}
				var li = document.createElement("li");
				li.innerHTML = msg;
				filelist.appendChild(li);
			}
		}
	</script>

    <h2>Sample 3 - Accessing uploaded files using Ajax</h2>
    <div class='subtitle'>This sample demonstrates how to process the uploaded files using Ajax instead of submiting the form.</div>
    
    <!-- this sample does not require form -->
    <%-- Html.BeginForm(); --%>
    
    <button id="uploadbutton" onclick="return false;">Select multiple files to upload</button>
    
    <!-- input type='hidden' id='myuploader' name='myuploader' -->
    <%=ViewData("uploaderhtml") %>
    
    <%-- Html.EndForm(); --%>
    
    <ol id="filelist">
    </ol>

</asp:Content>
