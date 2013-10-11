<%@ Control Language="C#" AutoEventWireup="true" Codebehind="NewPassword.ascx.cs"
	Inherits="Spinit.Wpc.Base.Presentation.Site.NewPassword" %>
<spinit:MessageManager ID="messageManager" runat="server" UniqueClientID="Base-MessageManager-ForgottenPassword"
	CaptionElement="h4" />
<asp:ValidationSummary CssClass="negative" ID="vsForgottenPassword" runat="server" ValidationGroup="Forgottenpassword" />
<fieldset>
	<legend>Begär nytt lösenord</legend>
	<p class="form-item">
		<asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" CssClass="labelLong">E-post:</asp:Label>
		<asp:TextBox ID="txtUserName" runat="server" ValidationGroup="Forgottenpassword" ToolTip="E-post är obligatoriskt" />
		<asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="E-post är obligatoriskt" ValidationGroup="Forgottenpassword" ControlToValidate="txtUserName" CssClass="error" ForeColor="White" Display="None"></asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator ID="vldreUsername" runat="server" ControlToValidate="txtUserName" CssClass="error" ErrorMessage="Du måste mata in en giltig e-postaddress!" ForeColor="White" ValidationGroup="Forgottenpassword" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None"></asp:RegularExpressionValidator></p>
	<p class="form-actions">
		<asp:ImageButton ID="imgBtnGetPassword" runat="server" ValidationGroup="Forgottenpassword" ImageUrl="/CommonResources/Images/Button-Send-Trans.png" AlternateText="Skicka" OnClick="btnGetPassword_Click" />
	</p>
</fieldset>
