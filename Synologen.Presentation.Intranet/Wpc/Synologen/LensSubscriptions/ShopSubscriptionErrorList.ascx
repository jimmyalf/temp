<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShopSubscriptionErrorList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions.ShopSubscriptionErrorList" %>
<asp:Repeater runat="server" DataSource='<%#Model.UnhandledErrors%>'>
<HeaderTemplate >
<table>
	<tr class="synologen-table-headerrow">
			<th>Datum</th><th>Typ av fel</th><th>Kund</th><th>Hantera</th>
	</tr>
</HeaderTemplate>
<ItemTemplate>
	<tr>
		<td><%# Eval("CreatedDate")%></td>
		<td><%# Eval("Reason")%></td>
		<td><%# Eval("CustomerName")%></td>
		<td><a href="<%# Eval("SubscriptionLink")%>" title="Hantera fel">Hantera</a></td>
	</tr>
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>