<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FtpProfileView>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Business.Domain.Enumerations" %>
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
				<%= Html.LabelFor(x => x.ServerURL) %>
				<%= Html.EditorFor(x => x.ServerURL) %>
				<%= Html.ValidationMessageFor(x => x.ServerURL) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.Username) %>
				<%= Html.EditorFor(x => x.Username) %>
				<%= Html.ValidationMessageFor(x => x.Username) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.Password) %>
				<%= Html.EditorFor(x => x.Password) %>
				<%= Html.ValidationMessageFor(x => x.Password) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.FtpProtocolType) %>
                <%= Html.DropDownListFor(x => x.SelectedFtpProtocolType, Model.GetFtpProtocolsSelectList(), new Dictionary<string, object> { {"data-bind", "options: selectableProtocols, optionsText: 'Name', optionsValue: 'Id', value: selectedProtocol"} }) %>
				<%= Html.ValidationMessageFor(x => x.FtpProtocolType) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.PassiveFtp) %>
				<%= Html.CheckBoxFor(x => x.PassiveFtp) %>
				<%= Html.ValidationMessageFor(x => x.PassiveFtp) %>
			</p>
			<p class="formCommands">
				<%= Html.AntiForgeryToken() %>
				<%= Html.HiddenFor(x => x.Id) %>
				<input type="submit" value="Save" class="btnBig" />
			</p>
		</fieldset>
    <% } %>
	<% Html.RenderPartial("ClientValidationScripts"); %>
