<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FrameColorListItemView>>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("FrameSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameBrand-Index">
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
     						column.For(x => x.NumberOfFramesWithThisColor).Named("Bågar med färgen"); 
							column.For(x => Html.ActionLink("Redigera","EditColor","Frame", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Redigera");
							column.For(x => Html.WpcGridDeleteForm(x, "DeleteColor", "Frame", new {id = x.Id})
									.OverrideButtonAttributes(title => "Radera färg"))
								.SetAsWpcControlColumn("Radera");
     					}
     				)
     				.Empty("Inga bågfärger i databasen.") %>
     			</div>
			</div>     						
		</div>				
	</div>
	<%=Html.WpcConfirmationDialog("Är du säker på att du vill radera vald bågfärg?") %>
</asp:Content>
