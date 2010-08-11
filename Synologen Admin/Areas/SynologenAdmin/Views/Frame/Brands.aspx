<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Core.Persistence.ISortedPagedList<FrameBrandListItemView>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
<div id="dCompMain" class="Components-Synologen-FrameColor-Index">
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
     						column.For(x => x.Name).Named("Färg");   						
							column.For(x => Html.ActionLink("Redigera","EditBrand","Frame", new {id = x.Id}, new object()))
								.Sortable(false)
								.Attributes(@class => "center")
								.Named("Redigera")
								.DoNotEncode()
								.HeaderAttributes(@class => "controlColumn");
							column.For("Radera").Named("Radera")
								.Partial("DeleteFrameBrand")
								.Sortable(false)
								.HeaderAttributes(@class => "controlColumn")
								.DoNotEncode();  								
     					}
     				)
     				.Empty("Inga bågfärger i databasen.") %>
     			</div>
			</div>     						
		</div>
	</div>
	<%=Html.WpcConfirmationDialog("Är du säker på att du vill radera valt bågmärke?") %>
</asp:Content>
