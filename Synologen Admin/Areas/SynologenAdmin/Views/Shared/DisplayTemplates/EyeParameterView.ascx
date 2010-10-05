<%@ Control Inherits="System.Web.Mvc.ViewUserControl<EyeParameterViewModel>" %>
<%=Html.LabelFor(x => x) %>
<%if (String.IsNullOrEmpty(Model.Format)) {%>
<span><%=Html.DisplayFor(x => x.Right) %></span>
<span><%=Html.DisplayFor(x => x.Left) %></span>
<%} else {%>
<span><%=Model.Right.ToString(Model.Format) %></span>
<span><%=Model.Left.ToString(Model.Format) %></span>
<%} %>