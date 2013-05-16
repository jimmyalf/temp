<%@ Page Language="C#" MasterPageFile="~/Wpc/Forum/Themes/MasterPageTemplate.master" Title="Forum" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <Forums:Message ID="Message1" runat="server" />
    </p>
</asp:Content>

