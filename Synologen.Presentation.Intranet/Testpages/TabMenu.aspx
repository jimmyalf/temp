<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<WpcSynologen:MenuControl runat="server" DisableLinksAfterSelectedItem="true">
	<HeaderTemplate><nav id="tab-navigation"><ul></HeaderTemplate>
	<Items>
		<WpcSynologen:MenuItem PageId="190"><span>1</span> Välj Kund</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="190"><span>2</span> Skapa Beställning</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="190" Url="/Testpages/TabMenu.aspx"><span>3</span> Betalningssätt</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="190"><span>4</span> Autogiro Information</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="190"><span>5</span> Bekräfta</WpcSynologen:MenuItem>
	</Items>
	<FooterTemplate></ul></nav></FooterTemplate>
</WpcSynologen:MenuControl>

</asp:Content>