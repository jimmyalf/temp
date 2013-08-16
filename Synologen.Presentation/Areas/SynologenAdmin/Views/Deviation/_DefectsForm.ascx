<%@ Control Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.Presentation.Models.Deviation.CategoryFormView>" %>

<fieldset>
    <legend>Fel</legend>
    <%
        for (var i = 0; i < Model.Defects.Count(); i++)
        {
    %>
    <p>
        <%= Html.CheckBoxFor(m => m.Defects[i].IsSelected) %>
        <%= Html.DisplayFor(m => m.Defects[i].Name) %>
        <%= Html.HiddenFor(m => m.Defects[i].Id) %>
    </p>
    <%
        }
    %>
    <p>
        <%= Html.EditorFor(x => Model.DefectName) %>
        <input type="button" id="add-defect-button" value="Lägg till fel till denna kategori" />
    </p>

</fieldset>
