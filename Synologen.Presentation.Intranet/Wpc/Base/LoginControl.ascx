<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Site.LoginControl" Codebehind="LoginControl.ascx.cs" %>
<div id="dError" runat="server"><strong>Wrong username or password! Try again.</strong></div>
<div id="dMain">
<fieldset>
	<legend><asp:Literal runat="server" Text="<%$Resources:LoginLegend%>" /></legend>
	<div class="formItem">
		<asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" CssClass="labelLong" Text="<%$Resources:Username%>" />
		<asp:TextBox ID="txtUserName" runat="server" />
	</div>
	<div class="formItem">
		<asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword" CssClass="labelLong" Text="<%$Resources:Password%>" />
		<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
	</div>
	<div class="formItem">
		<asp:CheckBox ID="chkRemember" runat="server" Text="<%$Resources:RememberMe%>" />
	</div>
	<div class="formCommands">
		<asp:Button ID="btnLogin" runat="server" Text="<%$Resources:Login%>" SkinID="Big" OnClick="btnLogin_Click" />
	</div>
</fieldset>
</div>