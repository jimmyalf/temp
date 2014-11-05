<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<%@ Register Src="Wpc/Base/LogoutControl.ascx" TagName="LogoutControl" TagPrefix="uc" %>
<%@ Register Src="~/CacheClearer.ascx" TagName="CacheClearer" TagPrefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Utloggningssida</title>
</head>
<body>
    <form id="form1" runat="server">
		<uc:CacheClearer id="ucCacheClearer" runat="server" />
		<uc:LogoutControl id="ucLogoutControl" runat="server">
		</uc:LogoutControl>
    </form>
</body>
</html>