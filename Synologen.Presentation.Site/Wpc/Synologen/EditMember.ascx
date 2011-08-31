<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditMember.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.EditMember" %>
<asp:PlaceHolder ID="plEditMember" runat="server" Visible="true">
<div id="synologen-edit-member" class="synologen-control">
<fieldset><legend>Medlemsuppgifter</legend>
	<p>
		<label>Namn</label><span>
		<strong><asp:Literal ID="ltMemberName" runat="server" /></strong></span>
	</p>
	<p>
		<label>E-post</label>
		<asp:TextBox ID="txtEmail" runat="server" />
	</p>
	<p>
		<label>Nytt lösenord</label>
		<asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" />
		<asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtNewPassword" ValidationGroup="vldSubmit" ErrorMessage="Lösenord får inte vara tomt." Display="Dynamic">*</asp:RequiredFieldValidator>
	</p>
	<p>
		<label>Verifiera</label>
		<asp:TextBox ID="txtNewPasswordVerify" runat="server" TextMode="Password" />
		<asp:CompareValidator id="cmpPasswords" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtNewPasswordVerify" ValidationGroup="vldSubmit" ErrorMessage="Lösenord och verifiering överensstämmer inte."  Display="Dynamic" >*</asp:CompareValidator>
	</p>
	<p>
		<label>Aktiv</label>
		<asp:CheckBox ID="chkActive" runat="server" />
	</p>
</fieldset>	
	<br />
	<div class="control-actions">
		<asp:Button ID="btnBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />
		<asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" ValidationGroup="vldSubmit" OnClientClick="return confirm('Är du säker på att du vill spara ändringar?');" />
	</div>
	<div class="error-summary">
		<asp:ValidationSummary id="valSummary" ValidationGroup="vldSubmit" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" />
		<br />
	 </div>	
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoAccessMessage" runat="server" Visible="false">
<p class="error-message"><strong>!</strong>&nbsp;Du har inte rätt att administrera vald användare.</p><br />
<asp:Button ID="btnErrorBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />
</asp:PlaceHolder>