<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Core.Persistence.ISortedPagedList<FrameGlassTypeListItemView>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
<div id="dCompMain" class="Components-Synologen-Frames-GlassTypes-Index">
	<%=Html.ValidationSummary("Ett fel har uppstått") %>
	<div class="fullBox">
		<div class="wrap">
			<div>
			<%=Html.WpcPager(Model)%>
				<%= Html.WpcGrid(Model)
					.Columns(
						column => {
     						column.For(x => x.Id).Named("ID")
     							.HeaderAttributes(@class => "controlColumn");
     						column.For(x => x.Name).Named("Glastyp");
							column.For(x => Html.ActionLink("Redigera","EditGlassType","Frame", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Redigera");
							column.For("Radera")
								.Partial("DeleteFrameGlassType")
								.SetAsWpcControlColumn("Radera");
     					}
     				)
     				.Empty("Inga glastyper i databasen.") %>
     			</div>
			</div>     						
		</div>				
	</div>
	<%=Html.WpcConfirmationDialog("Är du säker på att du vill radera vald glastyp?") %>
</asp:Content>
