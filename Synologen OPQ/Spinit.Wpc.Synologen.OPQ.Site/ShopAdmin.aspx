<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopAdmin.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.ShopAdmin" %>

<%@ Register src="Wpc/Synologen/OpqAdmin.ascx" tagname="OpqAdmin" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    	<uc1:OpqAdmin ID="OpqAdmin1" runat="server" ReturnPageUrl="/Spinit.Wpc.Synologen.OPQ.Site/Default.aspx" />
    
    </div>
    </form>
</body>
</html>
