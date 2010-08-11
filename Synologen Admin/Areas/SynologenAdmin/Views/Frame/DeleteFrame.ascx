<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameListItemView>" %>
<td class="center">
	<% using (Html.BeginForm("Delete","Frame", new { id = Model.Id })) { %>
		<%= Html.AntiForgeryToken() %>
		<input type="submit" value="Radera" class="btnSmall delete" title="Radera båge" />
	</form>
	<% } %>
</td>