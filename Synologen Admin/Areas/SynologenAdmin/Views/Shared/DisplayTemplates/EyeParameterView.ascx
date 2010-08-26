<%@ Control Inherits="System.Web.Mvc.ViewUserControl<EyeParameterViewModel>" %>
<div class="eye-parameters">
	<div class"parameter left">
		<%=Html.LabelFor(x => x.Left) %>
		<%=Html.DisplayFor(x => x.Left) %>
	</div>
	<div class"parameter right">
		<%=Html.LabelFor(x => x.Right) %>
		<%=Html.DisplayFor(x => x.Right) %>
	</div>
</div>