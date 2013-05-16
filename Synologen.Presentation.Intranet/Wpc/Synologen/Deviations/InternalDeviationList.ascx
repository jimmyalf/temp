<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternalDeviationList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.InternalDeviationList" %>

<fieldset>
    <asp:Repeater ID="rptInternalDeviation" runat="server" DataSource='<%#Model.Deviations%>'>
        <HeaderTemplate>
            <table>
                <tr>
                    <th>ID
                    </th>
                    <th>Kategori
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
                    <%# Eval("CreatedDate","{0:yyyy-MM-dd}")%>
                </td>
                <td class="center-cell">
                    <a href="<%= Model.ViewDeviationUrl %>?id=<%# Eval("Id")%>">Visa</a>
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
                    <%# Eval("CreatedDate","{0:yyyy-MM-dd}")%>
                </td>
                <td class="center-cell">
                    <a href="<%= Model.ViewDeviationUrl %>?id=<%# Eval("Id")%>">Visa</a>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</fieldset>
