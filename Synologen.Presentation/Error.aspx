<%@ Page Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Error" Codebehind="Error.aspx.cs" %>
<%@ Register Src="common/Copyright.ascx" TagName="Copyright" TagPrefix="uc1" %>
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
	<link rel="stylesheet" type="text/css" href="common/css/Style.css" media="screen,projection,print" />
</head>
<body id="<%= Spinit.Wpc.Base.Business.Globals.ProductBodyId %>" class="error-aspx">
<form id="ErrorForm" runat="server" method="post" action="#">
<div id="dErrorContainer">
	<h1>An error occured!</h1>

	<div id="dError">
		<h2>Error on page</h2>
		<p><asp:Literal id="ltErrorPage" runat="server"></asp:Literal></p>
    </div>
    
	<div id="dErrorDetails">
		<h2>Error text</h2>
		<p><asp:Literal id="ltErrorText" runat="server"></asp:Literal></p>
	</div>    

	<div id="dErrorFooter">
		<div id="dCopyright"><uc1:Copyright ID="Copyright" runat="server" /></div>
	</div>
</div>
</form>
</body>
</html>