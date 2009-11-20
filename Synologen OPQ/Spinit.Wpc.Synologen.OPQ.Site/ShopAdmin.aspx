<%@ Page Title="AdminPage" Language="C#" MasterPageFile="~/Opq.Master" AutoEventWireup="true" CodeBehind="ShopAdmin.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.ShopAdmin" %>

<%@ Register src="Wpc/Synologen/OpqAdmin.ascx" tagname="OpqAdmin" tagprefix="uc1" %>

<%@ Register src="Wpc/Synologen/OpqMenu.ascx" tagname="OpqMenu" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	    <uc2:OpqMenu ID="OpqMenu1" runat="server" OpqSubPageUrl="/Spinit.Wpc.Synologen.OPQ.Site/SubPage.aspx" />    
    	<uc1:OpqAdmin ID="OpqAdmin1" runat="server" ReturnPageUrl="/Spinit.Wpc.Synologen.OPQ.Site/Default.aspx" />
</asp:Content>
