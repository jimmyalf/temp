<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<p>
	<WpcSynologen:MigrateSubscription runat="server" ReturnPageId="1000" NewSubscriptionPageId="1008" />
</p>
</asp:Content>
