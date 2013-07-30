<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Core.Domain.Model.Deviations.Deviation>" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model.Deviations" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Extensions" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
    <% Html.RenderPartial("DeviationSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="dCompMain">
        <div class="fullBox">
            <div class="wrap">
                <div>
                    <fieldset>
                        <legend>Avvikelse <%= Model.Id %></legend>
                        <p>
                            Typ: <%= Model.Type.ToTypeOrDefault<DeviationType>().GetEnumDisplayName() %>
                        </p>
                        <p>
                            Status: <%= Model.Status.ToTypeOrDefault<DeviationStatus>().GetEnumDisplayName() %>
                        </p>
                        <%
                            if (Model.Type == DeviationType.External)
                            {
                        %>
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
                        <%
                            }

                        %>
                        <%
                            if (Model.Type == DeviationType.Internal)
                            {
                        %>
                        <p>
                            Rubrik: <%= Model.Title %>
                        </p>
                        <% }

                        %>
                        <% if (!string.IsNullOrEmpty(Model.DefectDescription))
                            {
                        %>
                        <p>
                            Övrigt: <%= Model.DefectDescription %>
                        </p>
                        <% }

                        %>
                        <%
                           if (Model.Type == DeviationType.External)
                           {
                        %>
                        <p>
                            <strong>Fel:</strong>
                        </p>
                        <%= Html.Grid(Model.Defects)
                                .Columns(
                                    column => {
                                        column.For(x => x.Name).Named("Namn");
                                    }
                                )
                                .Empty("Inga fel för denna avvikelsen.") %>
                        <%
                           }
                        %>
                        <p>
                            <strong>Kommentar:</strong>
                        </p>
                        <%= Html.Grid(Model.Comments.OrderByDescending(x => x.CreatedDate))
					.Columns(
						column => {
     						column.For(x => x.Description).Named("Kommentar");
                            column.For(x => x.CreatedDate).Named("Datum");
     					}
     				)
     				.Empty("Inga kommentarer för denna avvikelsen.") %>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <%= Html.ActionLink("Tillbaka till Avvikelser", "Index") %>
</asp:Content>

