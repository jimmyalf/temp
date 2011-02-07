<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
	<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<style type="text/css">
	#page-list a { display:block; }
</style>
<div id="page-list" style="clear:both; margin:15px;">
	<fieldset><legend>Avtalsförsäljningar</legend>
		<a href="/Testpages/SalesList.aspx">Lista försäljningar</a>
		<a href="/Testpages/CreateOrder.aspx">Registrera ny order</a>	
		<a href="/Testpages/SettlementList.aspx">Utbetalningar till butik</a>
		<a href="/Testpages/ViewSettlementPage.aspx?settlementId=1">Utbetalning 1 (ny) till butik</a>
		<a href="/Testpages/ViewSettlementOldPage.aspx?settlementId=1">Utbetalning 1 (gammal) till butik</a>
	</fieldset>
	<fieldset><legend>Gemensamt</legend>
		<a href="/Testpages/ShopList.aspx">Butikslistor</a>
		<a href="/Testpages/CityShopList.aspx">Ort-Butiklista</a>
		<a href="/Testpages/MembersList.aspx">Butiksmedlemmar</a>
		<a href="/Testpages/EditShopPage.aspx">Editera butik</a>
		<a href="/Testpages/SecurityTimeoutPage.aspx">Säkerhetstimout sida</a>
		<a href="Testpages/TestButtonPage.aspx">Validerings-sida</a>
	</fieldset>
	<fieldset><legend>Glasögonbeställning</legend>
		<a href="/Testpages/FrameOrder.aspx">Skapa glasögonbeställning</a>
		<a href="/Testpages/FrameOrderView.aspx?frameorder=1">Visa glasögonbeställning för order 1</a>
		<a href="/Testpages/FrameOrderView.aspx?frameorder=145">Visa glasögonbeställning för order 145</a>
		<a href="/Testpages/FrameOrderView.aspx?frameorder=-1">Visa glasögonbeställning för order -1</a>
		<a href="/Testpages/FrameOrderList.aspx">Visa beställningslista</a>
	</fieldset>
	<fieldset><legend>Linsabonnemang</legend>
		<a href="/Testpages/LensSubscriptionCustomersList.aspx">Visa linsabonnemang kunder</a>
		<a href="/Testpages/LensSubscriptionCreateSubscription.aspx?customer=1">Skapa linsabonnemang för kund(med id=1)</a>
		<a href="/Testpages/LensSubscriptionEditSubscription.aspx?subscription=1">Redigera linsabonnemang 1</a>
		<a href="/Testpages/LensSubscriptionCreateCustomer.aspx">Skapa kund för linsabonnemang</a>
		<a href="/Testpages/LensSubscriptionEditCustomer.aspx?customer=1">Redigera kund för linsabonnemang (med id=1)</a>
		<a href="/Testpages/LensSubscriptionTransactionsList.aspx?subscription=1">Visa transaktioner för linsabonnemang (med id=1)</a>
		<a href="/Testpages/LensSubscriptionShopSubscriptionErrorsList.aspx">Visa alla ohanterade fel för butik</a>
	</fieldset>
</div>
</asp:Content>