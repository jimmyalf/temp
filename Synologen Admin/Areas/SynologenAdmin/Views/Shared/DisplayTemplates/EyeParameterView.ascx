<%@ Control Inherits="System.Web.Mvc.ViewUserControl<EyeParameterViewModel>" %>
<br />
<span class="parameter right">
	<%=Html.LabelFor(x => x.Right) %>
	<%=Html.DisplayFor(x => x.Right) %>
</span>
<br />
<span class="parameter left">
	<%=Html.LabelFor(x => x.Left) %>
	<%=Html.DisplayFor(x => x.Left) %>
</span>