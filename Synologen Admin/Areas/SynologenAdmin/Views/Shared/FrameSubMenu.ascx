<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li>
			<%=Html.ActionLink("Bågar","Index","Frame") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","Add","Frame") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Bågfärger","Colors","Frame") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddColor","Frame") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Bågmärken","Brands","Frame") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddBrand","Frame") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Glastyper","GlassTypes","Frame") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddGlassType","Frame") %>
		</li>		
	</ul>
</div>