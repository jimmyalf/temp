<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intran�t" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<WpcSynologen:FrameOrder ID="synologenMvpTestControl" runat="server" RedirectPageId="5" />
</asp:Content>