<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<ArticleListView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Orders">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Artiklar</legend>
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
 						column.For(x => x.ArticleId).Named("ID").HeaderAttributes(@class => "controlColumn");
 						column.For(x => x.Name).Named("Artikel");
					    column.For(x => x.Type).Named("Typ");
					    column.For(x => x.Supplier).Named("Leverantör");
						column.For(x => x.RenderCheckboxFor(prop=> prop.Active)).DoNotEncode().SetAsWpcControlColumn("Aktiv");
						column.For(x => Html.ActionLink("Redigera", "ArticleForm", "Order", new {id = x.ArticleId}, new object())).SetAsWpcControlColumn("Redigera");
						column.For(x => Html.WpcGridDeleteForm(x, "DeleteArticle", "Order", new {id = x.ArticleId}).OverrideButtonAttributes(title => "Radera artikel")).SetAsWpcControlColumn("Radera");
 					}).Empty("Inga artiklar i databasen.")
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
