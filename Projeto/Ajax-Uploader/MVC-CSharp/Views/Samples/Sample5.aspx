<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sample 5 - Keep state after submit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Sample 5 - Keep state after submitting</h2>
    <div class="subtitle">This sample shows how to keep the uploaded files after submitting many times.</div>

	<script type="text/javascript">
		function CuteWebUI_AjaxUploader_OnPostback()
		{
			//submit the form after the file have been uploaded:
			document.forms[0].submit();
		}
	</script>
	
	<% Html.BeginForm(); %>
    
    <button id="uploadbutton" onclick="return false;">Select multiple files to upload</button>
    <!-- input type='hidden' id='myuploader' name='myuploader' -->
    <%=ViewData["uploaderhtml"] %>
    
    <%=ViewData["listhtml"] %>
    
    <br /><br /><br /><br />
    <input type='submit' value="do submit" />
    Now is <%=DateTime.Now.ToString("HH:mm:ss") %>
    
    <% Html.EndForm(); %>

    
</asp:Content>
