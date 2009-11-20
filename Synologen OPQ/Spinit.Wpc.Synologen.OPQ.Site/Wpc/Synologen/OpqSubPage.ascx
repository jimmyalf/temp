<%@ Import Namespace="Spinit.Wpc.Synologen.OPQ.Site.Code" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqSubPage.ascx.cs"
	Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqSubPage" %>
<opq:UserMessageManager ID="userMessageManager" ControlId="Opq-UserMessage-SubPage"
	runat="server" Visible="false" />
<h1 class="opq-header">
	<asp:Literal ID="ltParent" runat="server" /></h1>
<div class="opq-editable-area">
	<div id="opq-central-routine">
		<p>
			<span class="meta-date align-right">Senast Uppdaterad:
				<asp:Literal ID="ltCentralRoutineChangedDate" runat="server" />&nbsp;|&nbsp;av
				<asp:Literal ID="ltCentralRoutineChangedBy" runat="server" /></span></p>
		<div class="opq-central-routine-content">
			<asp:Literal ID="ltCentralRoutine" runat="server" /></div>
	</div>
	<div id="opq-shop-routine">
		<asp:PlaceHolder ID="phEditShopRoutine" runat="server"><a href="<%=AdminPageUrl %>?action=<%=Enumerations.AdminActions.EditRoutine%>&nodeId=<%=NodeId %>"
			class="align-right">Redigera egen rutin &raquo;</a> </asp:PlaceHolder>
		<h3 class="opq-sub-header">
			Egen rutin:</h3>
		<asp:Literal ID="ltShopRoutine" runat="server" />
	</div>
</div>
<div class="opq-editable-area">
	<div id="opq-central-documents">
		<h2 class="opq-sub-header">
			Centrala dokument:</h2>
		<asp:Repeater ID="rptCentralDocuments" OnItemDataBound="rptCentralDocuments_ItemDataBound"
			runat="server">
			<HeaderTemplate>
				<table class="table">
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td>
						<asp:Literal ID="ltCentralDocument" runat="server" />
					</td>
					<td>
						<asp:Literal ID="ltCentralDocumentDate" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table></FooterTemplate>
		</asp:Repeater>
	</div>
	<div id="opq-shop-routine-documents">
		<asp:PlaceHolder ID="phEditShopRoutineDocument" runat="server"><a href="<%=AdminPageUrl %>?action=<%=Spinit.Wpc.Synologen.OPQ.Site.Code.Enumerations.AdminActions.EditFiles%>&nodeId=<%=NodeId %>"
			class="align-right">Redigera egna dokument &raquo;</a> </asp:PlaceHolder>
		<h3 class="opq-sub-header">
			Egna dokument:</h3>
		<asp:Repeater ID="rptShopRoutineDocuments" OnItemDataBound="rptShopRoutineDocuments_ItemDataBound"
			runat="server">
			<HeaderTemplate>
				<table class="table">
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td>
						<asp:Literal ID="ltShopRoutineDocument" runat="server" />
					</td>
					<td>
						<asp:Literal ID="ltShopRoutineDocumentDate" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table></FooterTemplate>
		</asp:Repeater>
	</div>
	<div id="opq-shop-documents">
		<asp:PlaceHolder ID="phEditShopDocument" runat="server"><a href="<%=AdminPageUrl %>?action=<%=Spinit.Wpc.Synologen.OPQ.Site.Code.Enumerations.AdminActions.EditFiles%>&nodeId=<%=NodeId %>"
			class="align-right">Redigera ifyllda dokument &raquo;</a> </asp:PlaceHolder>
		<h3 class="opq-sub-header">
			Ifyllda dokument:</h3>
		<asp:Repeater ID="rptShopDocuments" OnItemDataBound="rptShopDocuments_ItemDataBound"
			runat="server">
			<HeaderTemplate>
				<table class="table">
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td>
						<asp:Literal ID="ltShopDocument" runat="server" />
					</td>
					<td>
						<asp:Literal ID="ltShopDocumentDate" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table></FooterTemplate>
		</asp:Repeater>
	</div>	
</div>
<div class="opq-editable-area">
	<div id="improvements">
		<h3 class="opq-sub-header">
			Posta ett förslag på förbättringsåtgärd:</h3>
		<p>
			<asp:TextBox ID="txtImprovements" runat="server" Rows="10" Columns="40" TextMode="MultiLine" /></p>
		<p>
			<asp:Button ID="btnSend" runat="server" Text="Skicka" OnClick="btnSend_Click" /></p>
	</div>
</div>
