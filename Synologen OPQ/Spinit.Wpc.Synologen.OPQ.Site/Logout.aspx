<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Logout" %>
<%@ Register Src="wpc/base/LogoutControl.ascx" TagName="LogoutControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:LogoutControl ID="LogoutControl1" runat="server" RedirectPage="/Spinit.Wpc.Synologen.OPQ.Site/Default.aspx" />
    </div>
    </form>
</body>
</html>
