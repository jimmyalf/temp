<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.Connections" Codebehind="Connections.ascx.cs" %>
<div class="Files-Connections-ascx">
<table class="striped" summary="List of connections associated with file or folder">
<caption>List of connections for file/folder</caption>
<thead>
<tr>
    <th>Name</th>
    <th>Description</th>
    <th>Component</th>
</tr>
</thead>
<tbody>
<tr id="rowNoConnections" runat="server"><td colspan="3">No connections found</td></tr>
<asp:Repeater ID="rptConnections" runat="server" DataMember="vwBaseFileObjects">
<ItemTemplate>
<tr>
    <td><%# DataBinder.Eval (Container.DataItem, "ObjName")%></td>
    <td><%# DataBinder.Eval (Container.DataItem, "ObjDescription")%></td>
    <td><%# DataBinder.Eval (Container.DataItem, "Component")%></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>