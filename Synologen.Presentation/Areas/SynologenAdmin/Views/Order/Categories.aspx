<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<CategoryListView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Orders">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Kategorier</legend>
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
 						column.For(x => x.CategoryId).Named("ID").HeaderAttributes(@class => "controlColumn");
 						column.For(x => x.Name).Named("Kategori");
						column.For(x => Html.ActionLink("Redigera", "CategoryForm", "Order", new {id = x.CategoryId}, new object())).SetAsWpcControlColumn("Redigera");
 					}).Empty("Inga kategorier i databasen.")
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
