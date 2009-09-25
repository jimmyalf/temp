<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<h2>Secure Page!</h2>
	<span>Secure login times out at: <%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SecurityIsValidUntil.ToString()%></span><br />
	<span>Seconds until logout: <%=(Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SecurityIsValidUntil-DateTime.Now).Seconds %></span>
	<synologen:SecurityLogout ID="ucSecurityLogout" runat="server" />
</asp:Content>
