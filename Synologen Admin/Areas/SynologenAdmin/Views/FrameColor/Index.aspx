<%@ Page MasterPageFile="~/Components/Synologen/SynologenMain.master" Inherits="System.Web.Mvc.ViewPage<FrameColorListView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
<div id="dCompMain" class="Components-Synologen-Frames">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Bågar</legend>
				<p>
					<%= Html.LabelFor(x => x.SelectedFrameColorName) %>
					<%= Html.EditorFor(x => x.SelectedFrameColorName) %>
					<%= Html.ValidationMessageFor(x => x.SelectedFrameColorName) %>
				</p>
				<p class="formCommands">
					<%= Html.AntiForgeryToken() %>
					<%= Html.HiddenFor(x => x.SelectedFrameColorId) %>				
					<input type="submit" value="Lägg till färg" class="btnBig" />
				</p>
			</fieldset>
			<% } %>
			<div>
				<%=Html.WpcPager(Model.List)%>
			</div>
			<div>
				<%= Html.WpcGrid(Model.List)
					.Columns(
						column => {
     						column.For(x => x.Id).Named("ID")
     							.HeaderAttributes(@class => "controlColumn");
     						column.For(x => x.Name).Named("Namn");
							column.For(x => Html.ActionLink("Redigera","Edit","Frame", new {id = x.Id}, new object()))
								.Sortable(false)
								.Attributes(@class => "center")
								.Named("Redigera")
								.DoNotEncode()
								.HeaderAttributes(@class => "controlColumn");
     					}
     				)
     				.Empty("Inga bågfärger i databasen.") %>
			</div>     						
		</div>				
	</div>
</div>
</asp:Content>
