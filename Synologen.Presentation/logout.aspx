<%@ Page language="c#" Inherits="Spinit.Wpc.Base.Presentation.logout" Codebehind="logout.aspx.cs" %>
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
	<link rel="stylesheet" type="text/css" href="common/css/Style.css" media="screen,projection,print" />
</head>
<body id="<%= Spinit.Wpc.Base.Business.Globals.ProductBodyId %>" class="logout-aspx">
<form id="LogoutForm" runat="server" method="post" action="#">
<div id="dLogoutContainer">
	<h1>Logging out...</h1>
</div>
</form>
</body>
</html>