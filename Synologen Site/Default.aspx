<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="clear:both; margin:15px;">
		<a href="SalesList.aspx">Lista försäljningar</a><br />
		<a href="CreateOrder.aspx">Registrera ny order</a>	<br />
		<a href="ShopList.aspx">Butikslistor</a><br />
		<a href="CityShopList.aspx">Ort-Butiklista</a><br />
		<a href="MembersList.aspx">Butiksmedlemmar</a><br />
		<a href="EditShopPage.aspx">Editera butik</a><br />
		<a href="SettlementList.aspx">Utbetalningar till butik</a><br />
		<a href="SecurityTimeoutPage.aspx">Säkerhetstimout sida</a><br />
		<a href="FrameOrder.aspx">Skapa glasögonbeställning</a><br />
		<a href="FrameOrderView.aspx?frameorder=1">Visa glasögonbeställning för order 1</a><br />
		<a href="FrameOrderView.aspx?frameorder=145">Visa glasögonbeställning för order 145</a><br />
		<a href="FrameOrderView.aspx?frameorder=-1">Visa glasögonbeställning för order -1</a><br />
		<a href="FrameOrderList.aspx">Visa beställningslista</a><br />
		<a href="LensSubscriptionCustomersList.aspx">Visa linsabonnemang kunder</a><br />
		<a href="LensSubscriptionCreateSubscription.aspx?customer=1">Skapa linsabonnemang för kund(med id=1)</a><br />
		<a href="LensSubscriptionCreateCustomer.aspx">Skapa kund för linsabonnemang</a>
	</div>
</asp:Content>