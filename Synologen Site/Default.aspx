<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<style type="text/css">
	#page-list a { display:block; }
</style>
<div id="page-list" style="clear:both; margin:15px;">
	<a href="SalesList.aspx">Lista försäljningar</a>
	<a href="CreateOrder.aspx">Registrera ny order</a>	
	<a href="ShopList.aspx">Butikslistor</a>
	<a href="CityShopList.aspx">Ort-Butiklista</a>
	<a href="MembersList.aspx">Butiksmedlemmar</a>
	<a href="EditShopPage.aspx">Editera butik</a>
	<a href="SettlementList.aspx">Utbetalningar till butik</a>
	<a href="SecurityTimeoutPage.aspx">Säkerhetstimout sida</a>
	<a href="FrameOrder.aspx">Skapa glasögonbeställning</a>
	<a href="FrameOrderView.aspx?frameorder=1">Visa glasögonbeställning för order 1</a>
	<a href="FrameOrderView.aspx?frameorder=145">Visa glasögonbeställning för order 145</a>
	<a href="FrameOrderView.aspx?frameorder=-1">Visa glasögonbeställning för order -1</a>
	<a href="FrameOrderList.aspx">Visa beställningslista</a>
	<a href="LensSubscriptionCustomersList.aspx">Visa linsabonnemang kunder</a>
	<a href="LensSubscriptionCreateSubscription.aspx?customer=1">Skapa linsabonnemang för kund(med id=1)</a>
	<a href="LensSubscriptionEditSubscription.aspx?subscription=1">Redigera linsabonnemang 1</a>
	<a href="LensSubscriptionCreateCustomer.aspx">Skapa kund för linsabonnemang</a>
	<a href="LensSubscriptionEditCustomer.aspx?customer=1">Redigera kund för linsabonnemang (med id=1)</a>
	<a href="LensSubscriptionTransactionsList.aspx?subscription=1">Visa transaktioner för linsabonnemang (med id=1)</a>
</div>
</asp:Content>