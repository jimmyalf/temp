<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site._Default" %>

<%@ Register src="Wpc/Synologen/OpqSubPage.ascx" tagname="OpqSubPage" tagprefix="uc1" %>

<%@ Register src="Wpc/Synologen/OpqMenu.ascx" tagname="OpqMenu" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<a href="Logout.aspx" >Logga ut</a><br />
	    <uc2:OpqMenu ID="OpqMenu1" runat="server" OpqSubPageUrl="/Spinit.Wpc.Synologen.OPQ.Site/Default.aspx" />
	    <uc1:OpqSubPage ID="OpqSubPage1" runat="server" AdminPageUrl="/Spinit.Wpc.Synologen.OPQ.Site/ShopAdmin.aspx" />
    </div>
    </form>
</body>
</html>
