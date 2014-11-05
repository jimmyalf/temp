<%@ Page Language="C#" MasterPageFile="~/commonresources/templates/Master/Forum Template.master" Title="Untitled Page" Inherits="Spinit.Wpc.Content.Presentation.Site.Code.PageControl" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <Forums:Logout runat="server" id="PostView1" />
<asp:Literal ID="ltPageId" Text="283" Visible="false" runat="server"/>
</asp:Content>
