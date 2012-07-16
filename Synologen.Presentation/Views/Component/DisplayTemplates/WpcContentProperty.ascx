<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcContentProperty>" %>
<% Html.RenderPartial(Model.InputType.ToString(), Model); %>