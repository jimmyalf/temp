<%@ Control Inherits="System.Web.Mvc.ViewUserControl<EyeParameterViewModel>" %>
<%=Html.LabelFor(x => x) %>
<%if (String.IsNullOrEmpty(Model.Format)) {%>
<span>
<%if(Model.DisplayRightValue){%>
	<%=Html.DisplayFor(x => x.Right) %>
<%}%>
</span>
<span>
<%if(Model.DisplayLeftValue){%>
	<%=Html.DisplayFor(x => x.Left) %>
<%}%>
</span>
<%} else {%>
<span>
<%if(Model.DisplayRightValue){%>
	<%=Model.Right.ToString(Model.Format)%>
<%}%>
</span>
<span>
<%if(Model.DisplayLeftValue){%>
	<%=Model.Left.ToString(Model.Format) %>
<%}%>
</span>
<%} %>