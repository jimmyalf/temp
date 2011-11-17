<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Site.LoginControl" Codebehind="LoginControl.ascx.cs" %>
<div id="dError" runat="server"><strong>Wrong username or password! Try again.</strong></div>
<div id="dMain">
<fieldset>
	<legend>Login</legend>
	<div class="formItem">
		<label for="txtUserName" class="labelLong">Username</label>
		<asp:TextBox ID="txtUserName" runat="server" MaxLength="20" />
	</div>
	<div class="formItem">
		<label for="txtPassword" class="labelLong">Password</label>
		<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="20" />
	</div>
	<div class="formItem">
		<asp:CheckBox ID="chkRemember" runat="server" Text="Remember me" />
	</div>
	<div class="formCommands">
		<asp:Button ID="btnLogin" runat="server" Text="Login" SkinID="Big" OnClick="btnLogin_Click" />
	</div>
</fieldset>
</div>