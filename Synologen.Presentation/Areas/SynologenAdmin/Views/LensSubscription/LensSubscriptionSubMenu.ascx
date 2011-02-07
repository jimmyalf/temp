<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li>
			<%=Html.ActionLink("Abonnemang","Index","LensSubscription") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Transaktionsartiklar","TransactionArticles","LensSubscription") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddTransactionArticle","LensSubscription") %>
		</li>		
	</ul>
</div>