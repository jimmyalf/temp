<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li>
			<%=Html.ActionLink("Avvikelser","Index","Deviation", new RouteValueDictionary(), null) %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Kategorier","ListCategories","Deviation") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddCategory","Deviation") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Leverantörer","ListSuppliers","Deviation") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddSupplier","Deviation") %>
		</li>
		<li>|</li>
	</ul>
</div>