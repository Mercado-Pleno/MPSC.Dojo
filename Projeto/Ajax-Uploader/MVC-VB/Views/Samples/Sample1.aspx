<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sample 1 - Simple Upload with Progress
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<script type="text/javascript">
		function CuteWebUI_AjaxUploader_OnPostback()
		{
			//submit the form after the file have been uploaded:
			document.forms[0].submit();
		}
	</script>

    <h2>Sample 1 - Simple Upload with Progress</h2>
    <div class='subtitle'>A basic sample demonstrating the use of the Upload control.</div>
    
    <% 	Html.BeginForm()%>
    
    <!-- input type='hidden' id='myuploader' name='myuploader' -->
    <%=ViewData("uploaderhtml")%>
    
    <% 	Html.EndForm()%>
    	  
	 <hr />
	  
    <!-- After posting the myuploader to server -->
    <%If ViewData("UploadedMessage") IsNot Nothing Then%>

	  <%=ViewData("UploadedMessage")%>
	  
    <%End If%>
    
    

</asp:Content>
