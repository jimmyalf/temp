﻿<%@ Control Language="C#" CodeBehind="ExternalDeviationList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.ExternalDeviationList" %>
<div class="synologen-control">
    <fieldset class="synologen-form">
        <asp:DropDownList
            ID="drpSuppliers"
            runat="server"
            DataSource='<%#Model.Suppliers%>'
            DataValueField="Id"
            DataTextField="Name"
            SelectedValue='<%#Model.SelectedSupplierId%>' AutoPostBack="True" />
        <%--<asp:Button runat="server" Text="Search" ID="btnSearch" />--%>
    
        <asp:Repeater ID="rptExternalDeviation" runat="server" DataSource='<%#Model.Deviations%>'>
            <HeaderTemplate>
                <table width="100%">
                    <tr>
                        <th>ID
                        </th>
                        <th>Kategori
                        </th>
                        <th>Leverantör
                        </th>
                        <th>Skapad Datum
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
                    <td class="center-cell">
                        <a href="<%=Model.ViewDeviationUrl %>?id=<%# Eval("Id")%>">Visa</a>
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
                    <td class="center-cell">
                        <a href="<%=Model.ViewDeviationUrl %>?id=<%# Eval("Id")%>">Visa</a>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </fieldset>
</div>