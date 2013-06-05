<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Page Language="C#" MasterPageFile="~/Wpc/Forum/Themes/MasterPageTemplate.master" Title="Forum" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <Forums:CreateEditPost runat="server"/>
</asp:Content>
