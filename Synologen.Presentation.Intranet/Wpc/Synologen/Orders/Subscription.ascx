<%@ Control Language="C#" CodeBehind="Subscription.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.Subscription" %>
<div id="synologen-order-subscription-" class="synologen-control">
	<fieldset class="synologen-form">
		<legend>Konto för <%=Model.CustomerName %></legend>
		<dl class="wide">
			<dt>Kontonr</dt>
			<dd><%#Model.BankAccountNumber %></dd>
			<dt>Clearingnr</dt>
			<dd><%#Model.ClearingNumber %></dd>
			<dt>Saldo</dt>
			<dd>Momsbelagt: <%#Model.TaxedBalance %>
	            <br/>
	           Momsfritt: <%#Model.TaxFreeBalance %>
			</dd>
			<dt>Medgivande</dt>
			<dd><%#Model.Consented %></dd>
			<dt>Abonnemang-status</dt>
			<dd><%#Model.Status %></dd>
			<dt>Skapat</dt>
			<dd><%#Model.CreatedDate %></dd>
		</dl>
		<p>
			<%if (Model.ShowStartButton) { %>
				<asp:Button runat="server" OnClick="Start_Subscription" Text="Starta"/>
			<% } %>
			<%if (Model.ShowStopButton) { %>
				<asp:Button runat="server" OnClick="Stop_Subscription" Text="Stoppa"/>
			<% } %>
		</p>
		<p>
			<a href="<%#Model.ReturnUrl %>">Tillbaka</a>
			&nbsp;
			<a href="<%#Model.CorrectionUrl %>">Korrigera saldo</a>
			<%if (Model.ShowResetDisplayUrl) { %>
			&nbsp;
			<a href="<%#Model.ResetSubscriptionUrl%>">Återstarta abonnemang</a>
			<% } %>
		</p>
		<asp:Repeater runat="server" DataSource='<%#Model.Errors%>' OnItemCommand="SetHandled_ItemCommand" Visible='<%#Model.HasErrors %>'>
			<HeaderTemplate>
			<table>
				<caption>Fel</caption>
				<thead><tr class="synologen-table-headerrow"><th>Typ</th><th>Datum</th><th>Hanterat</th></tr></thead>
				<tbody>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td><%#Eval("Type")%></td>
					<td><%#Eval("Created")%></td>
					<td><%# ((bool)Eval("IsHandled")) ? Eval("Handled") : ""%>
						<asp:Button CommandName='<%# Eval("Id")%>' Visible=<%# ! ((bool)Eval("IsHandled"))%> runat="server" Text="Markera som hanterad" OnClientClick="return confirm('Är du säker på att du vill markera felet som hanterat?');"  />
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</tbody>
			</table>
			</FooterTemplate>
		</asp:Repeater>
		
		<asp:Repeater runat="server" DataSource='<%#Model.SubscriptionItems%>'>
			<HeaderTemplate>
			<table>
				<caption>Delabonnemang</caption>
				<thead><tr class="synologen-table-headerrow"><th>Rubrik</th><th>Månadsbelopp</th><th>Dragningar</th><th>Skapat</th><th>Status</th><th>Detaljer</th></tr></thead>
				<tbody>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
				    <td><%#Eval("Title")%></td>
					<td><%#Eval("MontlyAmount")%></td>
					<td><%#Eval("PerformedWithdrawals")%></td>
					<td><%#Eval("CreatedDate")%></td>
					<td><%#Eval("Status")%></td>
					<td><a href="<%#Eval("SubscriptionItemDetailUrl")%>">Visa detaljer</a></td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</tbody>
			</table>
			</FooterTemplate>
		</asp:Repeater>
		
		<asp:Repeater runat="server" DataSource='<%#Model.Transactions%>'>
			<HeaderTemplate>
			<table>
				<caption>Transaktioner</caption>
				<thead><tr class="synologen-table-headerrow"><th>Beskrivning</th><th>Datum</th><th>Belopp</th><th>Utbetalad</th></tr></thead>
				<tbody>
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
				</tbody>
			</table>
			</FooterTemplate>
		</asp:Repeater>
	</fieldset>
</div>
