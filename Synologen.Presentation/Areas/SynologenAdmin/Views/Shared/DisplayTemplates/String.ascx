<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<span id="<%=ViewData.ModelMetadata.PropertyName %>"><%= Html.Encode(ViewData.TemplateInfo.FormattedModelValue) %></span>