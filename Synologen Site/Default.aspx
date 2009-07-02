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
		<a href="SecurityTimeoutPage.aspx">Säkerhetstimout sida</a>
	</div>
</asp:Content>