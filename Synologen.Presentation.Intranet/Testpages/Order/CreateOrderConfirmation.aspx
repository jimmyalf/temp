<%@ Page Language="C#"  MasterPageFile="~/Orders.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>

<p>
	<WpcSynologen:OrderCreateOrderConfirmation runat="server" PreviousPageId="1005" AbortPageId="1000" NextPageId="1012" />
</p>
</asp:Content>