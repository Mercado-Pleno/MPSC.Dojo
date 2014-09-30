<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Demo (VB)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Demo (VB)</h2>
	<div>
		<ol style="line-height:22px;">
			<li><%=Html.ActionLink("Simple Upload with Progress", "Sample1")%></li>
			<li><%=Html.ActionLink("Selecting multiple files for upload", "Sample2")%></li>
			<li><%=Html.ActionLink("Accessing uploaded files using Ajax", "Sample3")%></li>
			<li><%=Html.ActionLink("Start uploading manually", "Sample4")%></li>
			<li><%=Html.ActionLink("Keep state after submitting", "Sample5")%></li>
			<li><%=Html.ActionLink("AJAX file manager (Advanced)", "Advanced1")%></li>
		</ol>
	</div>
</asp:Content>
