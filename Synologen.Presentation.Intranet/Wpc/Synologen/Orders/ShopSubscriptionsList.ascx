<%@ Control Language="C#" CodeBehind="ShopSubscriptionsList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.ShopSubscripitonsList" %>
<div id="synologen-lens-subscription-summary-list" class="synologen-control">
<asp:Repeater ID="rptSubscriptions" runat="server" DataSource='<%#Model.List%>'>
	<HeaderTemplate >
		<table>
			<tr class="synologen-table-headerrow">
				<th>Kund</th><th>Månadsbelopp</th><th>Saldo</th><th>Status</th><th>Detaljer</th>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
			<tr>
				<td><a href="<%# Eval("CustomerDetailsUrl")%>"><%# Eval("CustomerName")%></a></td>
				<td><%# Eval("MonthlyAmount")%></td>
				<td><%# Eval("CurrentBalance")%></td>
				<td><%# Eval("Status")%></td>
				<td><a href="<%# Eval("SubscriptionDetailsUrl")%>">Visa detaljer</a></td>
			</tr>
	</ItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:Repeater>
</div>