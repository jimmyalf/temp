<%@ Control Language="C#" CodeBehind="ViewDeviation.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.ViewDeviation" %>
<h2>Visa Avvikelse</h2>
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
    <asp:Repeater ID="rptDefects" runat="server" DataSource='<%#Model.Defects%>'>
        <HeaderTemplate>
            <table>
                <tr>
                    <th>ID
                    </th>
                    <th>Namn
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
</fieldset>

<br />
<fieldset>
    <legend>Skriv kommentar</legend>
    <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine"></asp:TextBox>
    <p>
        <asp:Button ID="btnSaveComment" runat="server" Text="Spara" />
    </p>
    <p>
        <strong>Inlägg:</strong>
        <asp:Repeater ID="rptComments" runat="server" DataSource='<%#Model.Comments%>'>
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Kommentar
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# ((string)Eval("Description")).Replace("\n", "<br/>") %>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="synologen-table-alternative-row">
                    <td>
                        <%# ((string)Eval("Description")).Replace("\n", "<br/>") %>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </p>
</fieldset>

