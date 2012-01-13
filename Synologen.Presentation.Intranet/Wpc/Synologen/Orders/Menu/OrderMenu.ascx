<%@ Control Language="C#" AutoEventWireup="true"%>
<WpcSynologen:MenuControl runat="server" DisableLinksAfterSelectedItem="true" IncludeCurrentQuery="true">
	<HeaderTemplate><nav id="tab-navigation"><ul></HeaderTemplate>
	<Items>
		<WpcSynologen:MenuItem PageId="1001" DisableLink="True"><span>1</span> Välj Kund</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="1002"><span>2</span> Skapa Kund</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="1003"><span>3</span> Skapa Beställning</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="1004"><span>4</span> Betalningssätt</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="1005"><span>5</span> Autogiro Information</WpcSynologen:MenuItem>
		<WpcSynologen:MenuItem PageId="1006"><span>6</span> Bekräfta</WpcSynologen:MenuItem>
	</Items>
	<FooterTemplate></ul></nav></FooterTemplate>
</WpcSynologen:MenuControl>
