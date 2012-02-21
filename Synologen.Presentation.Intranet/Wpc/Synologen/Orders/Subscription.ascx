<%@ Control Language="C#" CodeBehind="Subscription.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.Subscription" %>
<div id="synologen-order-subscription-" class="synologen-control">
	<fieldset class="synologen-form">
		<legend>Konto för <%=Model.CustomerName %></legend>
		<p>
			<label>Kontonr</label>
			<span><%#Model.BankAccountNumber %></span>
		</p>
		<p>
			<label>Clearingnr</label>
			<span><%#Model.ClearingNumber %></span>
		</p>
		<p>
			<label>Medgivande</label>
			<span><%#Model.Consented %></span>
		</p>
		<p>
			<label>Status</label>
			<span><%#Model.Status %></span>
		</p>
		<p>
			<label>Skapat</label>
			<span><%#Model.CreatedDate %></span>
		</p>
		<%if (Model.HasErrors) {%>
		<asp:Repeater runat="server" DataSource='<%#Model.Errors%>' OnItemCommand="SetHandled_ItemCommand">
			<HeaderTemplate>
			<table>
				<caption>Fel</caption>
				<thead><tr class="synologen-table-headerrow"><th>Typ</th><th>Datum</th><th>Hanterat</th></tr></thead>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td><%#Eval("Type")%></td>
					<td><%#Eval("Created")%></td>
					<%if((bool)Eval("IsHandled")){%>
						<td><%#Eval("Handled")%></td>
					<%} else{ %>
						<td><asp:Button CommandName='<%# Eval("Id")%>'  Visible=<%# ! ((bool)Eval("IsHandled"))%> runat="server" Text="Markera som hanterad" OnClientClick="return confirm('Är du säker på att du vill markera felet som hanterat?');"  /></td>
					<% } %>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
			</table>
			</FooterTemplate>
		</asp:Repeater>
		<% } %>
		
		<asp:Repeater runat="server" DataSource='<%#Model.SubscriptionItems%>'>
			<HeaderTemplate>
			<table>
				<caption>Delabonnemang</caption>
				<thead><tr class="synologen-table-headerrow"><th>Belopp</th><th>Dragningar</th><th>Aktiv</th></tr></thead>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td><%#Eval("MontlyAmount")%></td>
					<td><%#Eval("PerformedWithdrawals")%></td>
					<td><%#Eval("Active")%></td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
			</table>
			</FooterTemplate>
		</asp:Repeater>
		
		<asp:Repeater runat="server" DataSource='<%#Model.Transactions%>'>
			<HeaderTemplate>
			<table>
				<caption>Transaktioner</caption>
				<thead><tr class="synologen-table-headerrow"><th>Beskrivning</th><th>Datum</th><th>Belopp</th><th>Utbetalad</th></tr></thead>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td><%#Eval("Description")%></td>
					<td><%#Eval("Date")%></td>
					<td><%#Eval("Amount")%></td>
					<td><%#Eval("IsPayed")%></td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
			</table>
			</FooterTemplate>
		</asp:Repeater>
	</fieldset>
</div>
