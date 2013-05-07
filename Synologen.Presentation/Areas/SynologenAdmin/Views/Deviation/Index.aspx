<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.Deviation.DeviationListView>" %>

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
                    <p class="formItem">
                        <%= Html.LabelFor(x => x.SelectedCategory) %>
                        <%= Html.DropDownListFor(x => x.SelectedCategory, new SelectList(Model.DeviationCategories, "Id", "Name"), "-- Alla kategorier --") %>
                    </p>
                    <p class="formItem">
                        <%= Html.LabelFor(x => x.SelectedSupplier) %>
                        <%= Html.DropDownListFor(x => x.SelectedSupplier, new SelectList(Model.DeviationSuppliers, "Id", "Name"), "-- Alla leverantörer --") %>
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
     						column.For(x => x.ShopId).Named("Butik");
                            column.For(x => x.CategoryName).Named("Kategori");
                            column.For(x => x.Type).Named("Typ");
                            column.For(x => x.SupplierName).Named("Leverantör");
                            column.For(x => x.CreatedDate).Named("Skapad");
							column.For(x => Html.ActionLink("Visa","ViewDeviation","Deviation", new {id = x.Id}, new object()))
								.SetAsWpcControlColumn("Visa");
     					}
     				)
     				.Empty("Inga avvikelser i databasen.") %>

                    <p>
                        •	Admin ska kunna filtrera i avvikelse-listan genom att använda den redan befintliga filter-funktionen i WPC.
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

