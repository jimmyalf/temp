<%@ Page Language="C#" MasterPageFile="~/commonresources/templates/Master/Forum Template.master" Title="Forum" Inherits="Spinit.Wpc.Content.Presentation.Site.Code.PageControl" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <Forums:UserProfile runat="server" ID="Userinfo1" NAME="Userinfo1" />
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
</asp:Content>

