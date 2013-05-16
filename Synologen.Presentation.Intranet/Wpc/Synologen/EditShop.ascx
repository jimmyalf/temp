<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditShop.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.EditShop" %>
<asp:PlaceHolder ID="plEditShop" runat="server" Visible="true">
<div id="synologen-edit-shop" class="synologen-control">
	<fieldset>
		<legend>Butiksuppgifter</legend>
		<p>
			<label>Hemsida</label>
			<asp:TextBox ID="txtWebsiteUrl" runat="server" CssClass="wide" />
		</p>
		<p>
			<label>Karta</label>
			<asp:TextBox ID="txtMapUrl" runat="server" CssClass="wide" />
		</p>
		<p>
			<label>Kontakt E-post</label>
			<asp:TextBox ID="txtEmail" runat="server" />
		</p>
		<p>
			<label>Giro-Typ</label>
			<asp:DropDownList id="drpGiroType" runat="server" DataValueField="cId" DataTextField="cName" />
		</p>
		<p>
			<label>Giro-Nummer</label>
			<asp:TextBox id="txtGiroNumber" runat="server" />
		</p>
		<div class="control-actions">
			<asp:Button ID="btnSave" Text="Spara" runat="server" OnClick="btnSave_OnClick" />
		</div>
		<p><asp:Literal ID="ltEventInformation" runat="server" /></p>
	</fieldset>
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoAccessMessage" runat="server" Visible="false">
<p class="error-message"><strong>!</strong>&nbsp;Du har inte rätt att administrera butik.</p><br />
</asp:PlaceHolder>