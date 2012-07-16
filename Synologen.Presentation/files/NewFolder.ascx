<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.NewFolder" Codebehind="NewFolder.ascx.cs" %>
<div class="Files-NewFolder-ascx">
<h2>New folder</h2>
<fieldset>
<legend>Create a new folder</legend>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtDirectory" SkinID="Long">Folder name</asp:Label>
	<asp:TextBox ID="txtDirectory" runat="server" Width="200px"></asp:TextBox>
</div>
<div class="formCommands">
	<asp:Button ID="btnCreateDirectory" runat="server" Text="Create" ToolTip="Create" OnClick="btnCreateDirectory_Click" />
	<asp:Button ID="btnCancelDirectory" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelDirectory_Click" />
</div>
</fieldset>
</div>