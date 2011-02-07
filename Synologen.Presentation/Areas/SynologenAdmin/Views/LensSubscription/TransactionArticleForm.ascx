<%@ Control Inherits="System.Web.Mvc.ViewUserControl<TransactionArticleModel>" %>
	<% Html.EnableClientValidation(); %>
	<%= Html.ValidationSummary(true) %>
	<% using (Html.BeginForm()) {%>
		<fieldset>
			<legend><%=Html.DisplayFor(x => x.FormLegend) %></legend>
			<p class="formItem">
				<%= Html.LabelFor(x => x.Name) %>
				<%= Html.EditorFor(x => x.Name) %>
				<%= Html.ValidationMessageFor(x => x.Name) %>
			</p>
			<p class="formItem">
				<%= Html.LabelFor(x => x.Active) %>
				<%= Html.EditorFor(x => x.Active) %>
				<%= Html.ValidationMessageFor(x => x.Active) %>
			</p>
			<p class="formCommands">
				<%= Html.AntiForgeryToken() %>
				<%= Html.HiddenFor(x => x.Id) %>
				<input type="submit" value="Save" class="btnBig" />
			</p>
		</fieldset>
    <% } %>
	<p>
		<a href='<%= Url.Action("TransactionArticles") %>'>Tillbaka till transaktionsartiklar &raquo;</a>
	</p>