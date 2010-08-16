<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameGlassTypeListItemView>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model"%>
<td class="center">
	<% using (Html.BeginForm("DeleteGlassType","Frame", new { id = Model.Id })) { %>
		<%= Html.AntiForgeryToken() %>
		<input type="submit" value="Radera" class="btnSmall delete" title="Radera glastyp" />
	</form>
	<% } %>
</td>