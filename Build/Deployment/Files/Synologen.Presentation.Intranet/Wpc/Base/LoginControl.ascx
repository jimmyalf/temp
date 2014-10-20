<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Site.LoginControl" Codebehind="LoginControl.ascx.cs" %>
<div id="dError" runat="server"><div class="error">Fel anv&auml;ndarnamn eller l&ouml;senord var god f&ouml;rs&ouml;k igen!</div></div>

	
	<div id="dMain"><legend>
	<h2 id="newsheader">Logga in </h2>
	</legend>
	<div class="formItem">
		<table width="300" align="center">
		  <tr><td align="right" width="50%"><label for="txtUserName" class="labelLong"><b>Anv&auml;ndarnamn</b></label>
		      &nbsp;</td>
	  <td align="left"><asp:TextBox ID="txtUserName" runat="server" MaxLength="100" /></td></tr></table>
	</div>
	<div class="formItem">
		<table width="300" align="center">
		  <tr><td align="right" width="50%"><label for="txtPassword" class="labelLong"><b>L&ouml;senord</b></label>
		      &nbsp;</td>
	  <td align="left"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="100" /></td></tr></table>
	</div>
	<div class="formCommands">
	  <asp:CheckBox ID="chkRemember" runat="server" Text="Komih&aring;g mig" />
	</div>
	<div class="formCommands">
		<asp:Button ID="btnLogin" runat="server" Text="Logga in" SkinID="Big" OnClick="btnLogin_Click" />
	</div>
	<div class="formCommands">
	  <a href="http://lev.synologen.nu">Till inloggning f&ouml;r v&aring;ra leverant&ouml;rer&raquo;      </a></div>
</div>