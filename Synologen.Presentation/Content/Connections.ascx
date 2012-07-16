<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.Connections" Codebehind="Connections.ascx.cs" %>
<div class="Content-Connections-ascx fullBox">
<div class="wrap">
<h2>Page Connections</h2>

<table class="striped" summary="List of connections associated with file or folder">
<caption>List of connections</caption>
<thead>
<tr>
    <th>Name</th>
    <th>Description</th>
    <th>Component</th>
    <th>Location</th>
</tr>
</thead>
<tbody>
<tr id="dNoConnections" runat="server"><td colspan="4">No connections found</td></tr>
<asp:Repeater ID="rptConnections" runat="server" OnItemCreated="rptConnections_ItemCreated">
<ItemTemplate>
<tr>
    <td><a href='<%# DataBinder.Eval (Container.DataItem, "ObjLink")%>'><%# DataBinder.Eval (Container.DataItem, "ObjName")%></a></td>
    <td><%# DataBinder.Eval (Container.DataItem, "ObjDescription")%></td>
    <td><%# DataBinder.Eval (Container.DataItem, "Component")%></td>
    <td><%# DataBinder.Eval (Container.DataItem, "ObjLocation")%></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
</div>