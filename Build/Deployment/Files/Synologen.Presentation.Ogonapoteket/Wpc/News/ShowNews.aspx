<%@ Page Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Site.ShowNews" Codebehind="ShowNews.aspx.cs" %>

<%@ Register Src="Show.ascx" TagName="Show" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Show ID="Show1" runat="server" />
    </form>
</body>
</html>
