<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CaixaEletronicoWeb._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<p>
		<asp:Label ID="lblSaque" runat="server" Text="Digite o Valor de Saque"></asp:Label>
		<asp:TextBox ID="textValorSaque" runat="server"></asp:TextBox>
		<asp:Button ID="btSolicitaSaque" runat="server" onclick="Button1_Click" 
            Text="Solicitar Saque" />
		<asp:Label ID="lblInformativo" runat="server" BorderStyle="Double" 
            Font-Bold="True" ForeColor="#CC3300"></asp:Label>
		<br />
	</p>
	<p>
		<asp:ListBox ID="lbListadeNotas" runat="server" Height="227px" Width="100px"></asp:ListBox>
	</p>
</asp:Content>
