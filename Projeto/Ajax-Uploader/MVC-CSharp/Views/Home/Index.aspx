<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        Ajax Uploader is an easy to use, hi-performance File Upload Control which allows you to upload files to web server without refreshing the page. 

It allows you select and upload multiple files and cancel running uploads, add new files during uploading. <br /><br />
    </p>
    <p>
        <%= Html.ActionLink("Demo(C#)", "Index", "Samples")%> | 
	    <a href="http://ajaxuploader.com/download/Ajax-Uploader.zip" title="Download ajax uploader">Download</a> | 
    	<a href="http://ajaxuploader.com/Order.aspx" title="Order now">Order now</a>
    </p>
</asp:Content>
