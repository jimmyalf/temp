<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternalDeviationList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.InternalDeviationList" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model.Deviations" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Extensions" %>
<div class="synologen-control">
    <fieldset class="synologen-form">
        <asp:Repeater ID="rptInternalDeviation" runat="server" DataSource='<%#Model.Deviations%>'>
            <HeaderTemplate>
                <table width="100%">
                    <tr>
                        <th>ID
                        </th>
                        <th>Rubrik
                        </th>
                        <th>Status</th>
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
                        <%# Eval("Title")%>
                    </td>
                    <td><%# Eval("Status").ToTypeOrDefault<DeviationStatus>().GetEnumDisplayName() %></td>
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
                        <%# Eval("Title")%>
                    </td>
                    <td><%# Eval("Status").ToTypeOrDefault<DeviationStatus>().GetEnumDisplayName() %></td>
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
</div>
