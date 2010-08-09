<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameBrandListItemView>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model"%>
<td class="center">
	<% using (Html.BeginForm("Delete","FrameBrand", new { id = Model.Id }, FormMethod.Post)) { %>
		<%= Html.AntiForgeryToken() %>
		<input type="submit" value="Radera" class="btnSmall" title="Radera bågmärke" />
	</form>
	<% } %>
</td>