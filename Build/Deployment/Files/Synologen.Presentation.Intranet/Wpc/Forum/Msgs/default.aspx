<%@ Page Language="C#" MasterPageFile="~/commonresources/templates/Master/Forum Template.master" Title="Forum" Inherits="Spinit.Wpc.Content.Presentation.Site.Code.PageControl" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <p>
        <Forums:Message ID="Message1" runat="server" />
    </p>
<asp:Literal ID="ltPageId" Text="283" Visible="false" runat="server"/>
</asp:Content>

