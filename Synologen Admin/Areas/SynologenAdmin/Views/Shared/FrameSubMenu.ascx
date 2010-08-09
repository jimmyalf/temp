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
			<%=Html.ActionLink("Bågfärger","Index","FrameColor") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","Add","FrameColor") %>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Bågmärken","Index","FrameBrand") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","Add","FrameBrand") %>
		</li>
	</ul>
</div>