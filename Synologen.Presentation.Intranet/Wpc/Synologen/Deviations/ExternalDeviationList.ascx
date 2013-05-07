<%@ Control Language="C#" CodeBehind="ExternalDeviationList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.ExternalDeviationList" %>

<p>
    <asp:DropDownList
        ID="drpSuppliers"
        runat="server"
        DataSource='<%#Model.Suppliers%>'
        DataValueField="Id"
        DataTextField="Name" 
        SelectedValue='<%#Model.SelectedSupplierId%>'
        />
    <asp:Button runat="server" Text="Search" ID="btnSearch" />
</p>


<fieldset>
    <legend>Externa avvikelser</legend>
    <asp:Repeater ID="rptExternalDeviation" runat="server" DataSource='<%#Model.Deviations%>'>
        <HeaderTemplate>
            <table>
                <tr>
                    <th>ID
                    </th>
                    <th>Kategori
                    </th>
                    <th>Leverantör
                    </th>
                    <th>Skapad Datum
                    </th>
                    <th>ShopId(temp)
                    </th>
                    <th></th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="center-cell">
                    <%# Eval("Id")%>
                </td>
                <td>
                    <%# Eval("Category.Name")%>
                </td>
                <td>
                    <%# Eval("Supplier.Name")%>
                </td>
                <td>
                    <%# Eval("CreatedDate","{0:yyyy-MM-dd}")%>
                </td>
                <td>
                    <%# Eval("ShopId")%>
                </td>
                <td class="center-cell">
                    <asp:PlaceHolder ID="plViewLink" runat="server"><a href="ViewDeviation.aspx?id=<%# Eval("Id")%>">Visa</a></asp:PlaceHolder>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="synologen-table-alternative-row">
                <td class="center-cell">
                    <%# Eval("Id")%>
                </td>
                <td>
                    <%# Eval("Category.Name")%>
                </td>
                <td>
                    <%# Eval("Supplier.Name")%>
                </td>
                <td>
                    <%# Eval("CreatedDate","{0:yyyy-MM-dd}")%>
                </td>
                <td>
                    <%# Eval("ShopId")%>
                </td>
                <td class="center-cell">
                    <asp:PlaceHolder ID="plViewLink" runat="server"><a href="ViewDeviation.aspx?id=<%# Eval("Id")%>">Visa</a></asp:PlaceHolder>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</fieldset>
