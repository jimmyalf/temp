<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="clear:both; margin:15px;">
		<h2>Register Order (Partnerweb)</h2>
		<ucSynologen:CreateOrder ID="createorder" runat="server" LocationId="6" LanguageId="1" />
	</div>

</asp:Content>