<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<WpcSynologen:MemberFilesThumbs ID="testMemberFilesThumbs" runat="server" LocationId="2"  />
</asp:Content>