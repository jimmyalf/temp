<%@ Page Title="" Language="C#" MasterPageFile="~/Opq.Master" AutoEventWireup="true" CodeBehind="NetCompetence.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.NetCompetence" %>
<%@ Register src="Wpc/Synologen/OpqNetCompetence.ascx" tagname="OpqNetCompetence" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<a href="Default.aspx">Tillbaka</a>&nbsp;<a href="Logout.aspx">Logga ut</a><br />
	<uc:OpqNetCompetence ID="OpqNetCompetence" runat="server" />
</asp:Content>
