<%@ Control Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<% if (Model.IsNotNullOrEmpty()) { %><span class="description"><%= Html.Encode(Model) %></span><% } %>