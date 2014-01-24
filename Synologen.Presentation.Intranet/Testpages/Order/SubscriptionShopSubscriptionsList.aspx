<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="float:left;margin:15px;clear:left;">
		<WpcSynologen:OrderShopSubscriptionsList runat="server" CustomerDetailsPageId="1007" SubscriptionDetailsPageId="1008"/>
	</div>
</asp:Content>