<%@ Page %>
<%@ Register TagPrefix="AspNetForums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" TagName="Faq" Src="faq.ascx" %>
<HTML>
    <HEAD>
        <AspNetForums:Style runat="server" ID="Style1" />
    </HEAD>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
      <Forums:Faq runat="server" />
    </body>
</HTML>
