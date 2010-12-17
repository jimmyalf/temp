﻿<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<TransactionArticleListView>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("LensSubscriptionSubMenu"); %>
<div id="dCompMain" class="Components-Synologen-LensSubscription-Transaction-Articles">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Transaktionsartiklar</legend>
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
			<%= Html.WpcPager(Model.Articles)%>
			<div>
				<%= Html.WpcGrid(Model.Articles).Columns( column => 
					{
 						column.For(x => x.ArticleId).Named("ID")
 							.HeaderAttributes(@class => "controlColumn");
 						column.For(x => x.Name).Named("Artikelnamn");
					    column.For(x => x.NumberOfConnectedTransactions).Named("Antal transaktioner");
					    column.For(x => x.Active).Named("Status");
						column.For(x => Html.ActionLink("Visa", "ViewTransactionArticle", "LensSubscription", new {id = x.ArticleId}, new object()))
							.SetAsWpcControlColumn("Redigera");
 					}).Empty("Inga abonnemang i databasen.")
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
