<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LensSubscriptionTransactionsList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptionTransactionsList" %>
<fieldset>
	<legend>Transaktioner</legend>
	<asp:Repeater ID="rptTransactions" runat="server" DataSource='<%#Model.List%>'>
		<HeaderTemplate >
			<table>
				<tr>
					<th>Typ</th><th>Orsak</th><th>Belopp</th><th>Datum</th>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
				<tr>
					<td><%# Eval("Type")%></td>
					<td><%# Eval("Reason")%></td>
					<td><%# Eval("Amount")%></td>
					<td><%# Eval("CreatedDate")%></td>
				</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</fieldset>