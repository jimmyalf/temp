<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.Deviation.CategoryListView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
    <% Html.RenderPartial("DeviationSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="dCompMain" class="Components-Synologen-FrameColor-Index">
        <div class="fullBox">
            <div class="wrap">
                <% using (Html.BeginForm()) {%>
                <fieldset>
                    <legend>Filtrera</legend>
                    <p class="formItem">
                        <%= Html.LabelFor(x => x.SearchTerm) %>
                        <%= Html.EditorFor(x => x.SearchTerm) %>
                    </p>
                    <p class="formCommands">
                        <%= Html.AntiForgeryToken() %>
                        <input type="submit" value="Sök" class="btnBig" />
                    </p>
                </fieldset>
                <% } %>
                <div>
                    <%=Html.WpcPager(Model.List)%>
                    <%= Html.WpcGrid(Model.List)
					.Columns(
						column => {
     						column.For(x => x.Id).Named("Id")
     							.HeaderAttributes(@class => "controlColumn");
     						column.For(x => x.Name).Named("Kategori");
                            column.For(x => x.RenderCheckboxFor(prop=> prop.Active)).DoNotEncode().SetAsWpcControlColumn("Aktiv");
							column.For(x => Html.ActionLink("Redigera","EditCategory","Deviation", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Redigera");
							column.For(x => Html.WpcGridDeleteForm(x, "DeleteCategory", "Deviation", new {id = x.Id})
									.OverrideButtonAttributes(title => "Radera kategori"))
								.SetAsWpcControlColumn("Radera");
     					}
     				)
     				.Empty("Inga kategorier i databasen.") %>

                    <%=Html.WpcConfirmationDialog("Är du säker på att du vill radera vald kategori?") %>
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>

