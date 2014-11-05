<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Login" %>
<%@ Register Src="wpc/base/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    	<a href="Logout.aspx" >Logga ut</a><br />
    	<uc1:LoginControl ID="LoginControl1" runat="server" RedirectPage="/Default.aspx" />
    </div>
    </form>
</body>
</html>
