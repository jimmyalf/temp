<%@ Page Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<html>
    <head>
        <Forums:PageTitle runat="server" ID="PageTitle1" />
        <Forums:Style runat="server" ID="Style1" />
    </head>
    <body>
        <form runat="server">
          <Forums:PostFlatView runat="server" SkinFileName="View-PostFlatViewPrint.ascx" />
        </form>
    </body>
</html>