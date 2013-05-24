<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FrameSupplierListItemView>>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-FrameColor-Index">
	<div class="fullBox">
		<div class="wrap">
			<div>
			<%=Html.WpcPager(Model)%>
				<%= Html.WpcGrid(Model)
					.Columns(
						column => {
     						column.For(x => x.Id).Named("ID")
     							.HeaderAttributes(@class => "controlColumn");
     						column.For(x => x.Name).Named("Namn");   						
     						column.For(x => x.Email).Named("Email");   
							column.For(x => Html.ActionLink("Redigera","EditSupplier","Frame", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Redigera");
							column.For(x => Html.WpcGridDeleteForm(x, "DeleteSupplier", "Frame", new {id = x.Id})
									.OverrideButtonAttributes(title => "Radera leverantör"))
								.SetAsWpcControlColumn("Radera");
     					}
     				)
     				.Empty("Inga leverantörer i databasen.") %>
     			</div>
			</div>     						
		</div>
	</div>
	<%=Html.WpcConfirmationDialog("Är du säker på att du vill radera vald leverantör?") %>
</asp:Content>

