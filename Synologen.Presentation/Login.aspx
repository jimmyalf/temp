<%@ Page language="c#" Inherits="Spinit.Wpc.Base.Presentation.Login" Codebehind="Login.aspx.cs" %>
<%@ Register Src="~/Common/Copyright.ascx" TagName="Copyright" TagPrefix="uc1" %>
<%@ Register Src="~/Common/StyleSheetLoader.ascx" TagName="SSLoader" TagPrefix="userControl" %>
<%@ Register Src="~/Common/JavaScriptLoader.ascx" TagName="JSLoader" TagPrefix="userControl" %>
<% if (Request.Browser.Browser.ToString() != "IE") { Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>"); } %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head runat="server">
	<title></title>
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<meta http-equiv="content-language" content="en" />
	<meta http-equiv="content-style-type" content="text/css" />
	<meta name="robots" content="none" />
	<meta name="copyright" content="Spinit AB" />
	<link rel="shortcut icon" href="common/img/wpc.ico" />
	<userControl:SSLoader ID="SSLoader" runat="server" />
	<userControl:JSLoader ID="JSLoader" runat="server" />
</head>
<body id="<%= Spinit.Wpc.Base.Business.Globals.ProductBodyId %>">
<form id="LoginForm" runat="server" method="post" action="#">
<div id="dLoginContainer" class="clearAfter">
	<div id="dHeader">
		<div id="dBranding"><img src="common/img/WPC_Logo_Login.png" alt="WPC logotype" title="Web Protal Component" /></div>
		<div id="dHeaderMessage"><h1>Welcome to WPC Administration</h1></div>
	</div>
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
	<div id="dFooter">
		<div id="dCopyright"><uc1:Copyright ID="Copyright" runat="server" /></div>
	</div>
</div>
</form>
</body>
</html>

	