<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<FrameOrderListView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Frames">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Glasögonbeställningar</legend>
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
				<%
               Html.WpcGrid(Model.List).Columns(column =>
               {
               	column.For(x => x.Id).Named("ID").HeaderAttributes(@class => "controlColumn");
               	column.For(x => x.Frame).Named("Båge");
               	column.For(x => x.GlassType).Named("Glastyp");
               	column.For(x => x.Shop).Named("Butik");
               	column.For(x => x.Created).Named("Skapad");
               	column.For(x => x.RenderCheckboxFor(property => property.Sent)).SetAsWpcControlColumn("Skickad");
               	column.For(x => Html.ActionLink("Visa", "ViewFrameOrder", "Frame", new {id = x.Id}, new object()))
               		.SetAsWpcControlColumn("Visa");
               }).Empty("Inga ordrar i databasen.").Render();
				%>
				<%=Html.WpcConfirmationDialog("Är du säker på att du vill radera vald order?") %>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>