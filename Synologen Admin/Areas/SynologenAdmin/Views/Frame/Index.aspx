<%@ Page MasterPageFile="~/Components/Synologen/SynologenMain.master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.FrameListView>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Presentation.Helpers"%>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
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
				<%=Html.WpcPager(Model.List).ExtraQueryParameters(new NameValueCollection{{"search", Model.SearchWord}})%>
			</div>
			<div>
				<%= Html.Grid(Model.List)
				    .Sort(Model.SortOptions)
					.Columns(
						column => {
     						column.For(x => x.Id).Named("Båg ID")
     							.HeaderAttributes( new Dictionary<string, object>{{"class","controlColumn"}} );
     						column.For(x => x.Name).Named("Namn");
						    column.For(x => x.ArticleNumber).Named("Artikelnr");
						    column.For(x => x.Brand).Named("Märke");
						    column.For(x => x.Color).Named("Färg");
							column.For(x => Html.ActionLink("Redigera","Edit","Frame", new {id = x.Id}, new object()))
								.Attributes(@class => "center")
								.Named("Redigera")
								.DoNotEncode()
								.HeaderAttributes( new Dictionary<string, object>{{"class","controlColumn"}} );
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
