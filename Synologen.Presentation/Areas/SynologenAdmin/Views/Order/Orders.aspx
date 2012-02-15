<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<OrderListView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Orders">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Beställningar</legend>
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
 						column.For(x => x.OrderId).Named("ID").HeaderAttributes(@class => "controlColumn");
 						column.For(x => x.ShopName).Named("Butik");
					    column.For(x => x.CustomerName).Named("Kund");
						column.For(x => x.PersonalIDNumber).Named("Kunds Personnummer");
						column.For(x => x.CreatedDate).Named("Skapad");
						column.For(x => Html.ActionLink("Visa", "OrderView", "Order", new {id = x.OrderId}, new object())).SetAsWpcControlColumn("Visa");
 					}).Empty("Inga beställningar i databasen.")
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
