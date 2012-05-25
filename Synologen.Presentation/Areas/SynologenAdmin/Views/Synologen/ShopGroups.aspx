<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<ShopGroupListView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("SynologenSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Subscriptions">
	<div class="fullBox">
		<div class="wrap">
			<%= Html.WpcPager(Model.List)%>
			<div>
				<%= Html.WpcGrid(Model.List).Columns( column => 
					{
 						column.For(x => x.Id).Named("ID").HeaderAttributes(@class => "controlColumn");
						column.For(x => x.Name).Named("Betalarnummer");
 						column.For(x => x.NumberOfShops).Named("Butik").Sortable(false);
						column.For(x => Html.ActionLink("Redigera", "ShopGroupForm", "Synologen", new {id = x.Id}, new object())).SetAsWpcControlColumn("Redigera");
						column.For(x => Html.WpcGridDeleteForm(x, "DeleteShopGroup", "Synologen", new {id = x.Id}).OverrideButtonAttributes(title => "Radera")).SetAsWpcControlColumn("Radera");
 					}).Empty("Inga butiksgrupper i databasen.")
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
