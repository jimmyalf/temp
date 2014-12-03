<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="float:left;margin:15px;clear:left;">
		<WpcSynologen:OrderSubscription runat="server" ReturnPageId="1000" SubscriptionItemDetailPageId="1009" CorrectionPageId="1010" SubscriptionResetPageId="1011" />
	</div>
</asp:Content>