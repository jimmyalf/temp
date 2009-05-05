<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="float:left;margin:15px;clear:left;">
		<h2>Category 1</h2>
		<ucSynologen:ShopList ID="shopList" runat="server" Category="1" />
	</div>
	<div style="float:left;margin:15px;">
		<h2>All</h2>	
		<ucSynologen:ShopList ID="shopList1" runat="server" />
	</div>
</asp:Content>