<%@ Control Inherits="System.Web.Mvc.ViewUserControl<ArticleView>" %>
	<% Html.EnableClientValidation(); %>
	<%= Html.ValidationSummary(true) %>
	<% using (Html.BeginForm()) {%>
		<fieldset>
			<legend><%=Html.Encode(Model.FormLegend) %></legend>
			<p class="formItem">
				<%= Html.LabelFor(x => x.Name) %>
				<%= Html.EditorFor(x => x.Name) %>
				<%= Html.ValidationMessageFor(x => x.Name) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.ArticleNumber) %>
				<%= Html.EditorFor(x => x.ArticleNumber) %>
				<%= Html.ValidationMessageFor(x => x.ArticleNumber) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.DefaultSPCSAccountNumber) %>
				<%= Html.EditorFor(x => x.DefaultSPCSAccountNumber) %>
				<%= Html.ValidationMessageFor(x => x.DefaultSPCSAccountNumber) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.Description) %>
				<%= Html.TextAreaFor(x => x.Description, new { @class= "txtAreaWide" }) %>
				<%= Html.ValidationMessageFor(x => x.Description) %>
			</p>
			<p class="formCommands">
				<%= Html.AntiForgeryToken() %>
				<%= Html.HiddenFor(x => x.Id) %>
				<input type="submit" value="Save" class="btnBig" />
			</p>
		</fieldset>
    <% } %>
	<p>
		<a href="<%=Model.ArticleListUrl %>">Tillbaka till artiklar</a>
	</p>
	<% Html.RenderPartial("ClientValidationScripts"); %>
