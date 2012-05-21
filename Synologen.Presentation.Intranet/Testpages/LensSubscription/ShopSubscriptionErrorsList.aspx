<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<div class="synologen-control">
	<fieldset class="synologen-form">
		<div style="float:left; clear:both">
			<h2>List with subscription page set</h2>
			<WpcSynologen:ShopSubscriptionErrorList runat="server" SubscriptionPageId="190" />
		</div>
		<div style="float:left; clear:both">
			<h2>List without subscription page set</h2>
			<WpcSynologen:ShopSubscriptionErrorList runat="server" />
		</div>
	</fieldset>
</div>
</asp:Content>


