<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FrameGlassTypeListItemView>>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Frames-GlassTypes-Index">
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
                            column.For(x => x.Supplier).Named("Leverantör");
							column.For(x => Html.ActionLink("Redigera","EditGlassType","Frame", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Redigera");
							column.For(x => Html.WpcGridDeleteForm(x, "DeleteGlassType", "Frame", new {id = x.Id})
									.OverrideButtonAttributes(title => "Radera glastyp"))
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
