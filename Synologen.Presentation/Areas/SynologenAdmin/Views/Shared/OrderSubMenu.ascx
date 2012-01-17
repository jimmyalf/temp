<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li>
			<%=Html.ActionLink("Beställningar","Orders","Order") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Kategorier","Categories","Order") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","CategoryForm","Order") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Leverantörer","Suppliers","Order") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","SupplierForm","Order") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Artikeltyper","ArticleTypes","Order") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","ArticleTypeForm","Order") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Artiklar","Articles","Order") %>
			&nbsp;>&nbsp; Ny
			<%=Html.ActionLink("Ny","ArticleForm","Order") %>
		</li>		
	</ul>
</div>