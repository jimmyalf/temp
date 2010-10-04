<%@ Control Inherits="System.Web.Mvc.ViewUserControl<EyeParameterViewModel>" %>
<%=Html.LabelFor(x => x) %>
<span><%=Html.DisplayFor(x => x.Right) %></span>
<span><%=Html.DisplayFor(x => x.Left) %></span>