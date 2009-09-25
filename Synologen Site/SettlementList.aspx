<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="clear:both; margin:15px;">
		<h2>SettlementList</h2>
		<synologen:SettlementList ID="settlementList" runat="server" />
	</div>
</asp:Content>
