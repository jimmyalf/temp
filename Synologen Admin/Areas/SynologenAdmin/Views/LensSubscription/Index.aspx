<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<SubscriptionListView>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("LensSubscriptionSubMenu"); %>
<div id="dCompMain" class="Components-Synologen-LensSubscriptions">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Linsabonnemang</legend>
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
 						column.For(x => x.SubscriptionId).Named("ID")
 							.HeaderAttributes(@class => "controlColumn");
 						column.For(x => x.ShopName).Named("Butik");
					    column.For(x => x.CustomerName).Named("Kund");
					    column.For(x => x.Status).Named("Status");
						column.For(x => Html.ActionLink("Redigera", "EditSubscription", "LensSubscription", new {id = x.SubscriptionId}, new object()))
							.SetAsWpcControlColumn("Redigera");
 					}).Empty("Inga abonnemang i databasen.")
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
