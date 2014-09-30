<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sample 4 - Start uploading manually
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<script type="text/javascript">
		function CuteWebUI_AjaxUploader_OnPostback()
		{
			//submit the form after the file have been uploaded:
			document.forms[0].submit();
		}
		function doStart()
		{
			var uploadobj = document.getElementById('myuploader');
			if (uploadobj.getqueuecount() > 0)
			{
				uploadobj.startupload();
			}
			else
			{
				alert("Please browse files for upload");
			}
		}
	</script>

    <h2>Sample 4 - Start uploading manually</h2>
    <div class='subtitle'>This sample demonstrates how to start uploading manually after file selection vs automatically.</div>
    
    <% Html.BeginForm() %>
    
    <button id="uploadbutton" onclick="return false;">Browse files</button>
    <!-- input type='hidden' id='myuploader' name='myuploader' -->
    <%=ViewData("uploaderhtml") %>
    
    <br /><br /><br />

    <button id="submitbutton" onclick="doStart();return false;">Submit</button>
    
    <% Html.EndForm() %>
    	  
	 <hr />
	  
    <!-- After posting the myuploader to server -->
    <%If ViewData("UploadedMessage") IsNot Nothing Then%>

	  <%=ViewData("UploadedMessage")%>
	  
    <%End If%>
    
    

</asp:Content>
