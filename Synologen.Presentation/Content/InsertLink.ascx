<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.InsertLink" Codebehind="InsertLink.ascx.cs" %>
<div class="Content-InsertLink-ascx">
<h2>Select link</h2>

<div class="clearLeft">
<asp:TreeView
	ID="treChooseLink"
	runat="server"
    OnSelectedNodeChanged="treChooseLink_SelectedNodeChanged"
    OnAdaptedSelectedNodeChanged="treChooseLink_SelectedNodeChanged"
	PathSeparator="#">
</asp:TreeView>
</div>

<div class="formCommands">
	<asp:Button ID="btnChoose" runat="server" Text="Choose" OnClick="btnChoose_Click" CausesValidation="False" />
	<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
</div>
</div>