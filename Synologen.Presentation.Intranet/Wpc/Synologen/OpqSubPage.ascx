<%@ Import Namespace="Spinit.Wpc.Synologen.OPQ.Site.Code" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqSubPage.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqSubPage" %>
<opq:usermessagemanager ID="userMessageManager" ControlId="Opq-UserMessage-SubPage" runat="server" />
<h1><asp:Literal ID="ltParent" runat="server" /></h1>
<div id="opq-central-routine">
	<asp:Literal ID="ltCentralRoutine" runat="server" />
	<span class="meta-date">Senast Uppdaterad: <asp:Literal ID="ltCentralRoutineChangedDate" runat="server" />&nbsp;|&nbsp;av <asp:Literal ID="ltCentralRoutineChangedBy" runat="server" /></span>
</div>
<div id="opq-shop-routine">
	Egen rutin:
	<asp:PlaceHolder ID="phEditShopRoutine" runat="server">
		<a href="<%=AdminPageUrl %>?action=<%=Enumerations.AdminActions.EditRoutine%>&nodeId=<%=NodeId %>">Redigera egen rutin &raquo;</a>
	</asp:PlaceHolder>

	<asp:Literal ID="ltShopRoutine" runat="server" />
</div>
<div id="opq-central-documents">
	Centrala dokument:
	<asp:Repeater ID="rptCentralDocuments" OnItemDataBound="rptCentralDocuments_ItemDataBound" runat="server">
	<HeaderTemplate><table></HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td><asp:Literal ID="ltCentralDocument" runat="server" /></td>
			<td><asp:Literal ID="ltCentralDocumentDate" runat="server" /></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate></table></FooterTemplate>
	</asp:Repeater>
</div>
<div id="opq-shop-routine-documents">
	Egna dokument:
	<asp:PlaceHolder ID="phEditShopDocument" runat="server">
		<a href="<%=AdminPageUrl %>?action=<%=Spinit.Wpc.Synologen.OPQ.Site.Code.Enumerations.AdminActions.EditFiles%>&nodeId=<%=NodeId %>">Redigera egna dokument &raquo;</a>
	</asp:PlaceHolder>
	<asp:Repeater ID="rptShopRoutineDocuments" OnItemDataBound="rptShopRoutineDocuments_ItemDataBound" runat="server">
	<HeaderTemplate><table></HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td><asp:Literal ID="ltShopRoutineDocument" runat="server" /></td>
			<td><asp:Literal ID="ltShopRoutineDocumentDate" runat="server" /></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate></table></FooterTemplate>
	</asp:Repeater>
</div>
<div id="improvements">
Posta ett förslag på förbättringsåtgärd:
<asp:TextBox ID="txtImprovements" runat="server" Rows="10" Columns="40" TextMode="MultiLine"/>
<asp:Button ID="btnSend" runat="server" Text="Skicka" onclick="btnSend_Click" />
</div>


