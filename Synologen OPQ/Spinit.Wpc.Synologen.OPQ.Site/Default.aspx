<%@ Page Title="Start Page" Language="C#" MasterPageFile="~/Opq.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site._Default" %>
<%@ Register src="Wpc/Synologen/OpqMenu.ascx" tagname="OpqMenu" tagprefix="uc2" %>
<%@ Register src="Wpc/Synologen/OpqStartPage.ascx" tagname="OpqStartPage" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<a href="Logout.aspx" >Logga ut</a><br />
	<uc2:OpqMenu ID="OpqMenu1" runat="server" OpqSubPageUrl="/Spinit.Wpc.Synologen.OPQ.Site/SubPage.aspx" NodeId="68" />
	<uc1:OpqStartPage ID="OpqStartPage1" runat="server" />
</asp:Content>
