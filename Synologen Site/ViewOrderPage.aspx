<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"  Title="Untitled Page" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="float:left;margin:15px;">
		<h2>View Order</h2>	
		<WpcSynologen:ViewOrder ID="editOrder" runat="server" />
	</div>
</asp:Content>
