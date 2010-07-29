<%@ Page MasterPageFile="~/Components/Synologen/SynologenMain.master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.FrameListView>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Presentation.Helpers"%>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li><%=Html.ActionLink("Lista","Index","Frame") %></li>
		<li><%=Html.ActionLink("Ny","Add","Frame") %></li>
	</ul>
</div>
<div id="dCompMain" class="Components-Synologen-Frames">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Bågar</legend>
				<p class="formItem">
					<%= Html.LabelFor(x => x.SearchWord) %>
					<%= Html.EditorFor(x => x.SearchWord) %>
				</p>
				<p class="formCommands">
					<input type="submit" value="Sök" class="btnBig" />
				</p>
			</fieldset>
			<% } %>
			<div>
				<%=Html.WpcPager(Model.List)
					.ExtraQueryParameters(new NameValueCollection{{"search",Model.SearchWord}})
				%>
			</div>
			<div>
				<%= Html.Grid(Model.List).Columns(
						column => {
     						column.For(x => x.Id).Named("Båg ID")
     							.HeaderAttributes( new Dictionary<string, object>{{"class","id-column"}} );
     						column.For(x => x.Name).Named("Namn");
     					}
     				)
     				.Attributes( new Dictionary<string, object>{{"class","striped"}} )
     				.HeaderRowAttributes( new Dictionary<string, object>{{"class","header"}} )
     				.Empty("Inga bågar i databasen.") %>
			</div>     						
		</div>				
	</div>
</div>
</asp:Content>
