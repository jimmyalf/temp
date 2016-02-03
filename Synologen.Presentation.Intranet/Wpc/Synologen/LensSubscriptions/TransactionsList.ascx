<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionsList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions.TransactionsList" %>
<fieldset>
	<legend>Transaktioner</legend>
	<% if (Model.HasTransactions) {%>
	<asp:Repeater ID="rptTransactions" runat="server" DataSource='<%#Model.List%>'>
		<HeaderTemplate >
			<table>
				<tr class="synologen-table-headerrow">
					<th>Typ</th><th>Orsak</th><th>Belopp</th><th>Datum</th><th>Utbetald</th>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
				<tr>
					<td><%# Eval("Type")%></td>
					<td><%# Eval("Reason")%></td>
					<td><%# Eval("Amount")%></td>
					<td><%# Eval("CreatedDate")%></td>
					<td><%# Eval("HasSettlement")%></td>
				</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<p>Saldo: <%=Model.CurrentBalance%></p>
	<% } 
		else { %><p>Det finns inga transaktioner att visa.</p><%
	} %>
</fieldset>