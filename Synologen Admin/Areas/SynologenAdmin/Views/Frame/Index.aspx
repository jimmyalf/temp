<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<FrameListView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
<div id="dCompMain" class="Components-Synologen-Frames">
	<%=Html.Messages() %>
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Bågar</legend>
				<p class="formItem">
					<%= Html.LabelFor(x => x.SearchTerm) %>
					<%= Html.EditorFor(x => x.SearchTerm) %>
				</p>
				<p class="formCommands">
					<input type="submit" value="Sök" class="btnBig" />
				</p>
			</fieldset>
			<% } %>
			<%= Html.WpcPager(Model.List)%>
			<div>
				<%= Html.WpcGrid(Model.List)
					.Columns(
						column => {
     						column.For(x => x.Id).Named("ID")
     							.HeaderAttributes(@class => "controlColumn");
     						column.For(x => x.Name).Named("Namn");
						    column.For(x => x.ArticleNumber).Named("Artikelnr");
						    column.For(x => x.Brand).Named("Märke");
						    column.For(x => x.Color).Named("Färg");
							column.For(x => Html.ActionLink("Redigera", "Edit", "Frame", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Redigera");
							column.For(x => Html.WpcGridDeleteForm(x, "Delete", "Frame", new {id = x.Id})
									.OverrideButtonAttributes(title => "Radera båge"))
								.SetAsWpcControlColumn("Radera");
     					}
     				)
     				.Empty("Inga bågar i databasen.")
				%>
				<%=Html.WpcConfirmationDialog("Är du säker på att du vill radera vald båge?") %>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>
