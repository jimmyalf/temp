<%@ Control Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.Presentation.Models.Deviation.CategoryFormView>" %>

<% Html.EnableClientValidation(); %>
<%= Html.ValidationSummary(true) %>
<% using (Html.BeginForm()) {%>
<fieldset>
    <legend><%= Html.DisplayFor(x => x.FormLegend) %></legend>
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
    <%
        if (Model.Id > 0)
        {
    %>
    <div id="deviation-defect-container">
        <% Html.RenderPartial("_DefectsForm", Model); %>
    </div>
    <%
        }
    %>
    <p class="formCommands">
        <%= Html.AntiForgeryToken() %>
        <%= Html.HiddenFor(x => x.Id) %>
        <input type="submit" value="Spara kategori" class="btnBig" />
    </p>
</fieldset>
<% } %>

<script>
    $(function () {

        $("#add-defect-button").live("click", function (e) {
            var name = $("#DefectName").val();
            if (name.length == 0)
                return;
            var url = '<%= Url.Action("AddDefect") %>';
            var postData = { defectName: $("#DefectName").val(), id: '<%=Model.Id%>' };
            $.post(url, postData, function (data) {
                if (data.success) {
                    $("#deviation-defect-container").html(data.partial);
                    $("#DefectName").val("");
                    $("#DefectName").focus();
                }
                else {
                    alert(data.errorMessage);
                }
            });
        });
    });
</script>

<% Html.RenderPartial("ClientValidationScripts"); %>

