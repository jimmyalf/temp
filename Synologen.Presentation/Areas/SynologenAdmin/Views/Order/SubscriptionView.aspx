<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.Order.SubscriptionView>" %>
<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-Order-View-aspx">
	<div class="fullBox">
		<div class="wrap">
		<fieldset>
			<legend>Abonnemang <%= Model.Id %></legend>
			<fieldset>
				<legend>Abonnemangsinformation</legend>
				<p><%= Html.LabelFor(x => x.Shop) %><%= Html.DisplayFor(x => x.Shop) %></p>
				<p><%= Html.LabelFor(x => x.Customer) %><%= Html.DisplayFor(x => x.Customer) %></p>
				<p><%= Html.LabelFor(x => x.AutogiroPayerNumber) %><%= Html.DisplayFor(x => x.AutogiroPayerNumber) %></p>
				<p><%= Html.LabelFor(x => x.AccountNumber) %><%= Html.DisplayFor(x => x.AccountNumber) %></p>
				<p><%= Html.LabelFor(x => x.ClearingNumber) %><%= Html.DisplayFor(x => x.ClearingNumber) %></p>
				<p><%= Html.LabelFor(x => x.CurrentBalance) %><%= Html.DisplayFor(x => x.CurrentBalance) %></p>
				<p><%= Html.LabelFor(x => x.Consented) %><%= Html.DisplayFor(x => x.Consented) %></p>
				<p><%= Html.LabelFor(x => x.Created) %><%= Html.DisplayFor(x => x.Created) %></p>
				<p><%= Html.LabelFor(x => x.Active) %><%= Html.DisplayFor(x => x.Active) %></p>
			</fieldset>
			<fieldset>
				<legend>Delabonnemang</legend>
				<table class="formItem striped">
					<thead>
						<tr><th>Månadsbelopp</th><th>Dragningar</th><th>Skapat</th><th>Status</th></tr>
					</thead>
					<tbody>
					<% foreach (var subscriptionItem in Model.SubscriptionItems){%>
						<tr>
							<td><%=subscriptionItem.MontlyAmount %></td>
							<td><%=subscriptionItem.PerformedWithdrawals %></td>
							<td><%=subscriptionItem.CreatedDate %></td>
							<td><%=subscriptionItem.Status %></td>
						</tr>
					<%} %>
					</tbody>
				</table>
			</fieldset>
			<fieldset>
				<legend>Transaktioner</legend>
				<table class="formItem striped">
					<thead>
						<tr><th>Beskrivning</th><th>Datum</th><th>Belopp</th><th>Utbetalad</th></tr>
					</thead>
					<tbody>
					<% foreach (var transaction in Model.Transactions){%>
						<tr>
							<td><%=transaction.Description %></td>
							<td><%=transaction.Date %></td>
							<td><%=transaction.Amount %></td>
							<td><%=transaction.IsPayed %></td>
						</tr>
					<%} %>
					</tbody>
				</table>
			</fieldset>
			<fieldset>
				<legend>Fel</legend>
				<table class="formItem striped">
					<thead>
						<tr><th>Typ</th><th>Datum</th><th>Hanterat</th></tr>
					</thead>
					<tbody>
					<% foreach (var error in Model.Errors){%>
						<tr>
							<td><%=error.Type %></td>
							<td><%=error.Created %></td>
							<td><%=error.Handled %></td>
						</tr>
					<%} %>
					</tbody>
				</table>
			</fieldset>
			<p class="display-item clearLeft">
				<a href='<%= Url.Action("Subscriptions") %>'>Tillbaka till abonnemang &raquo;</a>
			</p>
			</fieldset>
		</div>
	</div>
</div>	
</asp:Content>