<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li>
			<%=Html.ActionLink("Beställningar","Orders","Order") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Kategorier","Categories","Order") %>
			&nbsp;>&nbsp; Ny
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Leverantörer","Suppliers","Order") %>
			&nbsp;>&nbsp; Ny
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("ArtikelTyper","ArticleTypes","Order") %>
			 &nbsp;>&nbsp; Ny
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Artiklar","Articles","Order") %>
			&nbsp;>&nbsp; Ny
		</li>		
	</ul>
</div>