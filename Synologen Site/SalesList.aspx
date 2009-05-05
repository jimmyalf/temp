<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät"  %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<div style="clear:both; margin:15px;">
		<h2>List Orders (Wasa butik)</h2>
		<ucSynologen:SalesList ID="salesList" runat="server" />
	</div>
</asp:Content>