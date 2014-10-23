<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<form method="get" action="/butiker/sok-butik">
    <fieldset>
        <p><input type="text" name="search" placeholder="Postnummer eller ort" value="<%= ViewData.Model %>" /><input type="submit" value="Hitta" /></p>
    </fieldset>
</form>