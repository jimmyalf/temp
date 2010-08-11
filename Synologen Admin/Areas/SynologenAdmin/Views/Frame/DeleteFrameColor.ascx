<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameColorListItemView>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model"%>
<td class="center">
	<% using (Html.BeginForm("DeleteColor","Frame", new { id = Model.Id })) { %>
		<%= Html.AntiForgeryToken() %>
		<input type="submit" value="Radera" class="btnSmall delete" title="Radera bågfärg" <%=Model.DisableIf(x => x.DisableDelete) %> />
	</form>
	<% } %>
</td>