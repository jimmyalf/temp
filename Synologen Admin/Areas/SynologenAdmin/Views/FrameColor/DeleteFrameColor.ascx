<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameColorListItemView>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model"%>
<td class="center">
	<% using (Html.BeginForm("Delete","FrameColor", new { id = Model.Id })) { %>
		<%= Html.AntiForgeryToken() %>
		<input type="submit" value="Radera" class="btnSmall" title="Radera bågfärg" />
	</form>
	<% } %>
</td>