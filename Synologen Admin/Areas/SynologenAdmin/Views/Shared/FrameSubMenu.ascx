<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li><%=Html.ActionLink("Bågar","Index","Frame") %></li>
		<li><%=Html.ActionLink("Ny båge","Add","Frame") %></li>
		<li><%=Html.ActionLink("Färger","Index","FrameColor") %></li>
		<li><%=Html.ActionLink("Ny färg","Add","FrameColor") %></li>
	</ul>
</div>