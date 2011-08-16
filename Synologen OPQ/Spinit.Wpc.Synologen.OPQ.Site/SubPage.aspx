<%@ Page Title="SubPage" Language="C#" MasterPageFile="~/Opq.Master" AutoEventWireup="true" CodeBehind="SubPage.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.SubPage" %>

<%@ Register src="Wpc/Synologen/OpqSubPage.ascx" tagname="OpqSubPage" tagprefix="uc1" %>
<%@ Register src="Wpc/Synologen/OpqMenu.ascx" tagname="OpqMenu" tagprefix="uc2" %>

<%@ Register src="Wpc/Synologen/OpqSnurran.ascx" tagname="OpqSnurran" tagprefix="uc3" %>
<%@ Register src="Wpc/Synologen/OpqAtlas.ascx" tagname="OpqAtlas" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<a href="Logout.aspx" >Logga ut</a><br />
	    <uc3:OpqSnurran ID="OpqSnurran1" runat="server" OpqSubPageUrl="/SubPage.aspx" />
	    <uc2:OpqMenu ID="OpqMenu1" TypeOfDisplay="FromParent" runat="server" OpqSubPageUrl="/SubPage.aspx" />
	    <uc1:OpqSubPage ID="OpqSubPage1" runat="server" AdminPageUrl="/ShopAdmin.aspx" />
	    <uc4:OpqAtlas ID="OpqAtlas1" runat="server" />
</asp:Content>
