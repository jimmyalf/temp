<%@ Control Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.Presentation.Models.Deviation.SupplierFormView>" %>

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
        <%= Html.LabelFor(x => x.Email) %>
        <%= Html.EditorFor(x => x.Email) %>
        <%= Html.ValidationMessageFor(x => x.Email) %>
    </p>
    <p class="formItem">
        <%= Html.LabelFor(x => x.Phone) %>
        <%= Html.EditorFor(x => x.Phone) %>
        <%= Html.ValidationMessageFor(x => x.Phone) %>
    </p>
    <p class="formItem">
        <%= Html.LabelFor(x => x.Active) %>
        <%= Html.EditorFor(x => x.Active) %>
        <%= Html.ValidationMessageFor(x => x.Active) %>
    </p>
    <fieldset>
        <legend>Tillhör kategori</legend>
            <%
                for (var i = 0; i < Model.Categories.Count(); i++)
                {
            %>
            <p>
                <%= Html.CheckBoxFor(m => m.Categories[i].IsSelected) %>
                <%= Html.DisplayFor(m => m.Categories[i].Name) %>
                <%= Html.HiddenFor(m => m.Categories[i].Id) %>
            </p>
            <%
                }
            %>
    </fieldset>
    <p class="formCommands">
        <%= Html.AntiForgeryToken() %>
        <%= Html.HiddenFor(x => x.Id) %>
        <input type="submit" value="Spara leverantör" class="btnBig" />
    </p>
</fieldset>
<% } %>
<p>
    <%= Html.ActionLink("Tillbaka till Leverantörer", "Suppliers") %>
</p>
<% Html.RenderPartial("ClientValidationScripts"); %>