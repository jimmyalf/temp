<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" Title="Untitled Page" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Presentation.Intranet.Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<h2>Secure Page!</h2>
	<span>Secure login times out at: <%=SynologenSessionContext.SecurityIsValidUntil.ToString()%></span><br />
	<span>Seconds until logout: <%=(SynologenSessionContext.SecurityIsValidUntil-DateTime.Now).Seconds %></span>
	<WpcSynologen:SecurityLogout ID="ucSecurityLogout" runat="server" />
</asp:Content>
