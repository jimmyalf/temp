<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.Delete" Codebehind="Delete.ascx.cs" %>
<div class="Files-Delete-ascx">
<h2>Delete</h2>

<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="An error occured" Display="None" />
<p><strong>Do you which to delete?</strong></p>

<table class="striped" summary="List of files and folders to delete">
<caption>Files and folders to delete</caption>
<thead>
<tr>
	<th scope="col">Name</th>
	<th scope="col">Description</th>
</tr>
</thead>
<tbody>
<tr id="dNoFiles" runat="server"><td colspan="2">No files or folders selected</td></tr>
<asp:Repeater ID="rptConnections" runat="server" DataMember="tblConnections">
<ItemTemplate>
<tr>
	<td><%# DataBinder.Eval(Container.DataItem, "cName") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cDescription") %></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>

<div class="formCommands">
	<asp:Button ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete files and folders" OnClick="btnDelete_Click" />
	<asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel delete" OnClick="btnCancel_Click" />
</div>
</div>