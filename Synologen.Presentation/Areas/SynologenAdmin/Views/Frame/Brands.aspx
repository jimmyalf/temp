<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FrameBrandListItemView>>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameColor-Index">
	<%=Html.Messages() %>
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
     						column.For(x => x.NumberOfFramesWithThisBrand).Named("Bågar med märket");
							column.For(x => Html.ActionLink("Redigera","EditBrand","Frame", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Redigera");
							column.For(x => Html.WpcGridDeleteForm(x, "DeleteBrand", "Frame", new {id = x.Id})
									.OverrideButtonAttributes(title => "Radera märke"))
								.SetAsWpcControlColumn("Radera");
     					}
     				)
     				.Empty("Inga bågfärger i databasen.") %>
     			</div>
			</div>     						
		</div>
	</div>
	<%=Html.WpcConfirmationDialog("Är du säker på att du vill radera valt bågmärke?") %>
</asp:Content>
