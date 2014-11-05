<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.Order.SubscriptionListView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Subscriptions">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Abonnemang</legend>
				<p class="formItem">
					<%= Html.LabelFor(x => x.SearchTerm) %>
					<%= Html.EditorFor(x => x.SearchTerm) %>
				</p>
				<p class="formCommands">
					<%= Html.AntiForgeryToken() %>
					<input type="submit" value="Sök" class="btnBig" />
				</p>
			</fieldset>
			<% } %>
			<%= Html.WpcPager(Model.List)%>
			<div>
				<%= Html.WpcGrid(Model.List).Columns( column => 
					{
 						column.For(x => x.SubscriptionId).Named("ID").HeaderAttributes(@class => "controlColumn");
						column.For(x => x.PaymentNumber).Named("Betalarnummer");
 						column.For(x => x.Shop).Named("Butik");
						column.For(x => x.Customer).Named("Kund");
						column.For(x => x.AccountNumber).Named("Konto");
						column.For(x => x.Status).Named("Status").Sortable(false);
						column.For(x => Html.ActionLink("Visa", "SubscriptionView", "Order", new {id = x.SubscriptionId}, new object())).SetAsWpcControlColumn("Visa");
 					}).Empty("Inga abonnemang i databasen.")
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
