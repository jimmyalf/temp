<%@ Page Language="C#"  MasterPageFile="~/Orders.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<p>
	<WpcSynologen:OrderSearchCustomer runat="server" AbortPageId="1000" NextPageId="1002" />
</p>
</asp:Content>