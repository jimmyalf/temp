<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditShop.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.EditShop" %>
<asp:PlaceHolder ID="plEditShop" runat="server" Visible="true">
<div id="synologen-edit-shop" class="synologen-control">
	<fieldset><legend>Butiksuppgifter</legend>
		<label>Hemsida</label>
		<asp:TextBox ID="txtWebsiteUrl" runat="server" CssClass="wide" />
		<br />
		<label>Karta</label>
		<asp:TextBox ID="txtMapUrl" runat="server" CssClass="wide" />
		<br />
		<label>Kontakt E-post</label>
		<asp:TextBox ID="txtEmail" runat="server" />
		<br />							
		<label>Giro-Typ</label>
		<asp:DropDownList id="drpGiroType" runat="server" DataValueField="cId" DataTextField="cName" />
		<br />
		<label>Giro-Nummer</label>
		<asp:TextBox id="txtGiroNumber" runat="server" />
		<%--	
		<br />
		<label>Bank</label>
		<%--<asp:TextBox id="txtGiroSupplier" runat="server" />
		--%>
		<br />
		<label></label>
		<asp:Button ID="btnSave" Text="Spara" runat="server" OnClick="btnSave_OnClick" />
		<br />
		<p><asp:Literal ID="ltEventInformation" runat="server" /></p>
	</fieldset>
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoAccessMessage" runat="server" Visible="false">
<p class="error-message"><strong>!</strong>&nbsp;Du har inte rätt att administrera butik.</p><br />
</asp:PlaceHolder>