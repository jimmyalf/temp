<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<style type="text/css">
.synologen-form label,
.synologen-form input[type="submit"] { display:block; }
.synologen-form .readonly-parameter label { display:inline-block; width:8em; }
</style>
<p>
	<WpcSynologen:LensSubscriptionEditSubscription runat="server"  />
</p>
</asp:Content>


