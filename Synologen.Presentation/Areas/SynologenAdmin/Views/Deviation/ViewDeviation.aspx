<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Core.Domain.Model.Deviations.Deviation>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
    <% Html.RenderPartial("DeviationSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="dCompMain" class="Components-Synologen-FrameColor-Index">
        <div class="fullBox">
            <div class="wrap">
                <div>
                    <fieldset>
                        <legend>Avvikelse <%= Model.Id %></legend>
                        <p>
                            Typ: <%= Model.Type %>
                        </p>
                        <%
                            if (Model.Supplier != null)
                            {
                        %>
                        <p>
                            Leverantör: <%= Model.Supplier.Name %>
                        </p>
                        <%
                            }
                        %>
                        <p>
                            Kategori: <%= Model.Category.Name %>
                        </p>
                        <% if (!string.IsNullOrEmpty(Model.DefectDescription))
                            {
                        %>
                        <p>
                            Övrigt: <%= Model.DefectDescription %>
                        </p>
                        <% }

                        %>
                        <p>
                            <strong>Fel:</strong>
                        </p>
                        <%= Html.WpcGrid(Model.Defects)
					.Columns(
						column => {
     						column.For(x => x.Id).Named("Id")
     							.HeaderAttributes(@class => "controlColumn");
     						column.For(x => x.Name).Named("Namn");
     					}
     				)
     				.Empty("Inga fel för denna avvikelsen.") %>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <%= Html.ActionLink("Tillbaka till Avvikelser", "Index") %>

    <p>
        •	Admin ska kunna klicka på Nästa, Sista, Förgående och Första om det finns flera sidor/rader med avvikelser.
    </p>
</asp:Content>

