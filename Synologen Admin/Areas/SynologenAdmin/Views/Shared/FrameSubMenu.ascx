<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li><%=Html.ActionLink("Lista","Index","Frame") %></li>
		<li><%=Html.ActionLink("Ny","Add","Frame") %></li>
	</ul>
</div>