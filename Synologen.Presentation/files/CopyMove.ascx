<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.CopyMove" Codebehind="CopyMove.ascx.cs" %>
<div class="Files-CopyMove-ascx">
<h2>Copy/Move files</h2>

<fieldset>
<legend>Copy or move files</legend>
<div id="dMyLocations" runat="server" class="formItem">
	<asp:Label ID="Label1" runat="server" AssociatedControlID="drpLocations" SkinID="Long">Location</asp:Label>
	<asp:DropDownList ID="drpLocations" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpLocations_SelectedIndexChanged"></asp:DropDownList>
</div>

<div class="treeView">
<label>Choose target</label>
<asp:TreeView
	ID="treDirectories"
	runat="server" 
	OnSelectedNodeChanged="treDirectories_SelectedNodeChanged"
	OnAdaptedSelectedNodeChanged="treDirectories_SelectedNodeChanged">
    <RootNodeStyle ImageUrl="~/common/icons/Root-Node.png" />
    <ParentNodeStyle ImageUrl="~/common/icons/Folder-Closed.png" />
    <LeafNodeStyle ImageUrl="~/common/icons/Folder-Closed.png" />
    <SelectedNodeStyle ImageUrl="~/common/icons/Folder-Open.png" />
</asp:TreeView>
</div>

<div class="formCommands">
	<asp:Button ID="btnOk" runat="server" Text="Ok" ToolTip="Copy/Move to selected node" OnClick="btnOk_Click" />
	<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
</div>
</fieldset>
</div>