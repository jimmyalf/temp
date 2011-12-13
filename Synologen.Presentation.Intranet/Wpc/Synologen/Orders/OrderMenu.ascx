<%@ Control Language="C#" AutoEventWireup="true"%>
<WpcSynologen:MenuControl runat="server" DisableLinksAfterSelectedItem="true" IncludeCurrentQuery="true">
	<HeaderTemplate><nav id="tab-navigation"><ul></HeaderTemplate>
	<Items>
		<WpcSynologen:MenuItem PageId="775"><span>1</span> Välj Kund</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="773"><span>2</span> Skapa Kund</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="774"><span>3</span> Skapa Beställning</WpcSynologen:MenuItem>
		<%--
		<WpcSynologen:MenuItem PageId="190"><span>4</span> Autogiro Information</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="190"><span>5</span> Bekräfta</WpcSynologen:MenuItem>
		--%>
	</Items>
	<FooterTemplate></ul></nav></FooterTemplate>
</WpcSynologen:MenuControl>
