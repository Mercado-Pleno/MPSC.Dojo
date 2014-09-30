<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sample 2 - Selecting multiple files for upload 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<script type="text/javascript">
		function CuteWebUI_AjaxUploader_OnPostback()
		{
			//submit the form after the file have been uploaded:
			document.forms[0].submit();
		}
	</script>

    <h2>Sample 2 - Selecting multiple files for upload</h2>
    <div class='subtitle'>Ajax Uploader allows you select and upload multiple files and cancel running uploads, add new files during uploading.</div>
    
    <% 	Html.BeginForm()%>
    
    <button id="uploadbutton" onclick="return false;">Select multiple files to upload</button>
    <!-- input type='hidden' id='myuploader' name='myuploader' -->
    <%=ViewData("uploaderhtml") %>
    
    <% 	Html.EndForm()%>
    	  
	 <hr />
	  
    <!-- After posting the myuploader to server -->
    <%If ViewData("UploadedMessage") IsNot Nothing Then%>

	  <%=ViewData("UploadedMessage")%>
	  
    <%End If%>
    
    

</asp:Content>
