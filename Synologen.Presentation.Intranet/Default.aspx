<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
	<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<style type="text/css">
	#page-list a { display:block; }
	fieldset, legend { border: gainsboro solid 1px; margin-bottom: 10px; padding: 5px;}
	legend { font-size: 1.2em; margin-left: 10px; }
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
		<a href="/Testpages/LensSubscription/CustomersList.aspx">Visa linsabonnemang kunder</a>
		<a href="/Testpages/LensSubscription/CreateSubscription.aspx?customer=1">Skapa linsabonnemang för kund(med id=1)</a>
		<a href="/Testpages/LensSubscription/EditSubscription.aspx?subscription=1">Redigera linsabonnemang 1</a>
		<a href="/Testpages/LensSubscription/CreateCustomer.aspx">Skapa kund för linsabonnemang</a>
		<a href="/Testpages/LensSubscription/EditCustomer.aspx?customer=1">Redigera kund för linsabonnemang (med id=1)</a>
		<a href="/Testpages/LensSubscription/TransactionsList.aspx?subscription=1">Visa transaktioner för linsabonnemang (med id=1)</a>
		<a href="/Testpages/LensSubscription/ShopSubscriptionErrorsList.aspx">Visa alla ohanterade fel för butik</a>
		<a href="/Testpages/LensSubscription/ShopSubscriptionsList.aspx">Visa alla linsabonnemang för inloggad butik</a>
		<a href="/Testpages/LensSubscription/MigrateSubscription.aspx?subscription=1">Migrera abonnemang</a>
	</fieldset>
	<fieldset><legend>Leverantörskontroller</legend>
		<a href="Testpages/Supplier/MemberProfile.aspx">Member profile</a>
		<a href="Testpages/Supplier/AdminMemberFiles.aspx">Admin Member Files</a>
		<a href="Testpages/Supplier/AdminMemberpage.aspx">Admin Member Page</a>
	</fieldset>
	<fieldset>
	    <legend>Flöden</legend>
	    <a href="TestPages/YammerFeed.aspx">Flöde från Yammer</a>
	    <a href="TestPages/MiniYammerFeed.aspx">Begränsat flöde från Yammer</a>
	</fieldset>
    <fieldset>
	    <legend>Beställningar</legend>
        <a href="TestPages/Order/SearchCustomer.aspx">Sök kund</a>
		<span>--</span>
		<a href="TestPages/Order/SubscriptionShopSubscriptionsList.aspx">Lista abonnemang</a>
		<a href="Testpages/Order/EditCustomer.aspx?customer=1">Redigera kund</a>
	</fieldset>
</div>
</asp:Content>