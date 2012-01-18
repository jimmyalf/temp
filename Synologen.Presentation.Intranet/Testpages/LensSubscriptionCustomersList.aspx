<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<style type="text/css">
.synologen-form label,
.synologen-form input[type="submit"] { display:block; }
</style>
<p>
	<WpcSynologen:LensSubscriptionCustomersList EditPageId="190" runat="server"  />
</p>
</asp:Content>


