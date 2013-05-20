<%@ Control Language="C#" CodeBehind="ViewDeviation.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.ViewDeviation" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model.Deviations" %>
<div class="synologen-control">
    <h1>Visa Avvikelse</h1>
    <fieldset class="synologen-form">
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
        <%
       if (Model.Type == DeviationType.External)
       {
        %>
        <p>
            <strong>Fel:</strong>
        </p>
        <div class="synologen-deviation-list">
            <asp:Repeater ID="rptDefects" runat="server" DataSource='<%#Model.Defects%>'>
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <th width="10%">ID
                            </th>
                            <th width="90%">Namn
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="center-cell">
                            <%# Eval("Id")%>
                        </td>
                        <td>
                            <%# Eval("Name")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="synologen-table-alternative-row">
                        <td class="center-cell">
                            <%# Eval("Id")%>
                        </td>
                        <td>
                            <%# Eval("Name")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </fieldset>
    <%
       }
    %>
    <br />
    <fieldset>
        <legend>Skriv kommentar</legend>
        <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" Columns="20" Rows="2"></asp:TextBox>
        <p>
            <asp:Button ID="btnSaveComment" runat="server" Text="Spara" />
        </p>
        <p>
            <asp:Repeater ID="rptComments" runat="server" DataSource='<%#Model.Comments%>'>
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <th width="75%">Kommentar
                            </th>
                            <th width="25%">Datum
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# ((string)Eval("Description")).Replace("\n", "<br/>") %>
                        </td>
                        <td>
                            <%# Eval("CreatedDate") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="synologen-table-alternative-row">
                        <td>
                            <%# ((string)Eval("Description")).Replace("\n", "<br/>") %>
                        </td>
                        <td>
                            <%# Eval("CreatedDate") %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </p>
    </fieldset>
    <a href="javascript:history.back(-1);">Tillbaka</a>
</div>
