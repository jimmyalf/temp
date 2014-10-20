<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionMessage.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.ActionMessage" %>
<% if (Model.HasActionMessage) {%>
<div id="action-message">
	<p><%#Model.Message %></p>
</div>
<% } %>