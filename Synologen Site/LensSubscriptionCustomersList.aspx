<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<WpcSynologen:LensSubscriptionCustomersList ID="synologenMvpTestControl" EditPageId="190" runat="server"  />
</asp:Content>


