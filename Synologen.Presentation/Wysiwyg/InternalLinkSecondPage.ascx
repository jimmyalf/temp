<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.InternalLinkSecondPage" Codebehind="InternalLinkSecondPage.ascx.cs" %>
<div class="InternalLinkSecondPage-ascx">
<fieldset>
	<legend>Link properties</legend>

	<div class="formItem">
		<asp:Label ID="lblRdoPopup" runat="server" AssociatedControlID="rdoPopup" SkinID="Long">Popup</asp:Label>
		<asp:RadioButtonList ID="rdoPopup" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rdoPopup_SelectedIndexChanged">
			<asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
			<asp:ListItem Value="No" Text="No" Selected="True">No</asp:ListItem>
		</asp:RadioButtonList>
	</div>
</fieldset>

<fieldset>
	<legend>Popup size</legend>
	<div class="formItem  smallItem">
		<asp:Label ID="lblPosSizeWidth" runat="server" AssociatedControlID="txtPopSizeWidth" SkinID="Long">Widht (in pixels)</asp:Label>
		<asp:TextBox ID="txtPopSizeWidth" runat="server" Enabled="False"></asp:TextBox>
	</div>

	<div class="formItem smallItem">
		<asp:Label ID="lblPopSizeHeight" runat="server" AssociatedControlID="txtPopSizeHeight" SkinID="Long">Height (in pixels)</asp:Label>
		<asp:TextBox ID="txtPopSizeHeight" runat="server" Enabled="False"></asp:TextBox>
	</div>
</fieldset>

<fieldset>
	<legend>Popup location</legend>
	<div class="formItem smallItem clearLeft">
		<asp:Label ID="lblPopLocTop" runat="server" AssociatedControlID="txtPopLocTop" SkinID="Long">From the top (in pixels)</asp:Label>
		<asp:TextBox ID="txtPopLocTop" runat="server" Enabled="False"></asp:TextBox>
	</div>

	<div class="formItem smallItem">
		<asp:Label ID="lblPopLocLeft" runat="server" AssociatedControlID="txtPopLocLeft" SkinID="Long">From the left (in pixels)</asp:Label>
		<asp:TextBox ID="txtPopLocLeft" runat="server" Enabled="False"></asp:TextBox>
	</div>

	<div class="formItem">
		<asp:Label ID="lblPopUpLocation" runat="server" AssociatedControlID="drpPopUpLocation" SkinID="Long">Popup location</asp:Label>
		<asp:DropDownList ID="drpPopUpLocation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPopUpLocation_SelectedIndexChanged" Enabled="False">
			<asp:ListItem>None</asp:ListItem>
			<asp:ListItem>top</asp:ListItem>
			<asp:ListItem>top_left</asp:ListItem>
			<asp:ListItem>left</asp:ListItem>
		</asp:DropDownList>
	</div>
</fieldset>
</div>