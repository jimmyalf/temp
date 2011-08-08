<%@ Register Src="~/content/PageControl.ascx" TagName="Page" TagPrefix="cnt" %>
<%@ Register Src="~/content/PageLinkControl.ascx" TagName="PageLink" TagPrefix="cnt" %>
<%@ Register Src="~/content/PageProperties.ascx" TagName="PageProperties" TagPrefix="cnt" %>
<%@ Register Src="~/content/PageAdvancedProperties.ascx" TagName="PageAdvProperties" TagPrefix="cnt" %>
<%@ Register Src="~/content/Connections.ascx" TagName="PageConnections" TagPrefix="cnt" %>
<%@ Page Language="C#" MasterPageFile="~/content/ContentMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.EditPage" MaintainScrollPositionOnPostback="true" Codebehind="EditPage.aspx.cs" %>
<asp:Content ID="Page" ContentPlaceHolderID="ComponentPage" Runat="Server">
<div class="Content-EditPage-aspx fullBox">
<div class="wrap">
<h1>Content</h1>

<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="This is my message" Display="None" />

<p><strong><asp:Label ID="lblStatus" runat="server" Text="Document status: Default" /></strong></p>

<div id="dContentButtons">
	<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="false" />
	<asp:Button ID="btnSaveCheckIn" runat="server" Text="Save &amp; Publish" OnClick="btnSaveCheckIn_Click" Visible="false" />
	<asp:Button ID="btnSaveAs" runat="server" Text="Save As..." OnClick="btnSaveAs_Click" Visible="false" />
	<asp:Button ID="btnUndoCheckout" runat="server" Text="Undo check out" CausesValidation="false" OnClick="btnUndoCheckOut_Click" Visible="false" />
	<asp:Button ID="btnCheckOut" runat="server" Text="Check Out" CausesValidation="false" OnClick="btnCheckOut_Click" Visible="true" />
	<asp:HyperLink ID="lnkPreview" runat="server" NavigateUrl="~/content/Preview.aspx" rel="external">Preview</asp:HyperLink>
</div>

<asp:PlaceHolder ID="phPageMenu" runat="server" />

<div class="tabsContentContainer clearAfter">
<asp:MultiView ID="view" ActiveViewIndex="0" runat="server">
    <asp:View ID="PageView" runat="server">
        <cnt:Page ID="pgeMain" Visible="true" runat="server" />
        <cnt:PageLink ID="pgeMainLink" Visible="true" runat="server" />
    </asp:View>
    <asp:View ID="PropertiesView" runat="server">
		<cnt:PageAdvProperties ID="pgeAdvPropMain" Visible="true" runat="server" />
		<cnt:PageProperties ID="pgePropMain" Visible="true" runat="server" />
    </asp:View>
    <asp:View ID="ConnectionsView" runat="server">
		<cnt:PageConnections ID="pgeConnections" Visible="true" runat="server" PageTypeId="1" />
    </asp:View>
</asp:MultiView>
</div>

</div>
</div>
</asp:Content>