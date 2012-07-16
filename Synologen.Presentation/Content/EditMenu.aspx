<%@ Register Src="~/content/PageProperties.ascx" TagName="PageProperties" TagPrefix="cnt" %>
<%@ Register Src="~/content/MenuControl.ascx" TagName="MenuControl" TagPrefix="cnt" %>
<%@ Page Language="C#" MasterPageFile="~/content/ContentMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.EditMenu" MaintainScrollPositionOnPostback="true" Codebehind="EditMenu.aspx.cs" %>
<asp:Content ID="MenuContent" ContentPlaceHolderID="ComponentPage" Runat="Server">
<div class="Content-EditMenu-aspx fullBox">
<div class="wrap">
<h1>Content</h1>

<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="This is my message" Display="None" />

<p><strong><asp:Label ID="lblStatus" runat="server" Text="Document status: Default" /></strong></p>

<div class="clearLeft">
	<asp:Button ID="btnCheckOut" runat="server" Text="Check Out" CausesValidation="false" OnClick="btnCheckOut_Click" Enabled="true" />
	<asp:Button ID="btnUndoCheckout" runat="server" Text="Undo check out" CausesValidation="false" OnClick="btnUndoCheckOut_Click" Enabled="false" />
	<%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Enabled="false" />--%>
	<asp:Button ID="btnSaveCheckIn" runat="server" Text="Save &amp; Publish" OnClick="btnSaveCheckIn_Click" Enabled="false" />
	<asp:Button ID="btnSaveAs" runat="server" Text="Save As..." OnClick="btnSaveAs_Click" Enabled="false" />
	<asp:HyperLink ID="lnkPreview" runat="server" NavigateUrl="~/content/Preview.aspx" rel="external">Preview</asp:HyperLink>
</div>

</div>
</div>

<cnt:MenuControl ID="mnuMain" Visible="true" runat="server" />
<cnt:PageProperties ID="pgePropMain" Visible="true" runat="server" />

</asp:Content>