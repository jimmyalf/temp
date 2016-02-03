<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcMenuItem>" %>
<li class="selected"><a href="<%= Model.Url %>"><%= Model.Text %></a><% Html.RenderPartial("ChildMenu", Model); %></li>