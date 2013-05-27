<%@ Page Language="C#" MasterPageFile="~/Wpc/Forum/Themes/MasterPage.master" Title="Forum" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <Forums:UserProfile runat="server" ID="Userinfo1" NAME="Userinfo1" />
</asp:Content>

