<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>

<WpcSynologenCommon:TabMenu runat="server">
	<Items>
		<WpcSynologenCommon:TabMenuItem Text="Steg 1" PageId="1" />
		<WpcSynologenCommon:TabMenuItem Text="Steg 2" PageId="2"/>
	</Items>
</WpcSynologenCommon:TabMenu>

</asp:Content>