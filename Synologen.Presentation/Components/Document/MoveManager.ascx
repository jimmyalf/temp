<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MoveManager.ascx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.MoveManager" %>
<div class="Components-Document-MoveManager-ascx">
<h2>Move document</h2>
<fieldset>
<legend>Move from</legend>
    Move document <asp:Label ID="lblDocumentName" runat="server" Text=""></asp:Label> from folder <asp:Label ID="lblNodeName" runat="server" Text=""></asp:Label>.
</fieldset>
<fieldset>
<legend>Move to</legend>
<div id="dMyLocations" runat="server" class="formItem">
	<asp:Label ID="lblRootFolders" runat="server" AssociatedControlID="drpRootFolders" SkinID="Long">Root folder</asp:Label>
	<asp:DropDownList ID="drpRootFolders" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpRootFolders_SelectedIndexChanged" ></asp:DropDownList>
</div>

<div class="treeView">
<label>Choose target</label>
    <asp:TreeView ID="tvwDocNodes" runat="server" OnAdaptedSelectedNodeChanged="tvwDocNodes_SelectedNodeChanged" OnSelectedNodeChanged="tvwDocNodes_SelectedNodeChanged">
    <RootNodeStyle ImageUrl="~/common/icons/Root-Node.png" />
    <ParentNodeStyle ImageUrl="~/common/icons/Folder-Closed.png" />
    <LeafNodeStyle ImageUrl="~/common/icons/Folder-Closed.png" />
    <SelectedNodeStyle ImageUrl="~/common/icons/Folder-Open.png" />
    </asp:TreeView>

</div>
</fieldset>

<div class="formCommands">
	<asp:Button ID="btnOk" runat="server" Text="Ok" ToolTip="Move to selected node" OnClick="btnOk_Click" />
	<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
</div>

</div>

