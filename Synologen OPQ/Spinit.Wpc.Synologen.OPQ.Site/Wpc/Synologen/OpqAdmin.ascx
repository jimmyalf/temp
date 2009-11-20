<%@ Import Namespace="Spinit.Wpc.Synologen.OPQ.Site.Code" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqAdmin.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqAdmin" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg"
	TagPrefix="uc1" %>
<opq:UserMessageManager ID="userMessageManager" ControlId="Opq-UserMessage-AdminPage"
	runat="server" Visible="false" />
<h2 class="opq-sub-header">
	<asp:Literal ID="ltAdminHeader" runat="server" Text="Redigera" /></h2>
<asp:PlaceHolder ID="phEditRoutine" runat="server" Visible="false">
	<uc1:WpcWysiwyg ID="_wysiwyg" runat="server" Mode="Basic" />
	<asp:Button ID="btnSaveForLater" runat="server" OnClick="btnSave_Click" CommandName="SaveForLater"
		Text="Spara" />
	<asp:Button ID="btnSaveAndPublish" runat="server" OnClick="btnSave_Click" CommandName="SaveAndPublish"
		Text="Spara & Publicera" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="phEditDocuments" runat="server" Visible="false">
	<div class="opq-editable-area">
		<div id="opq-shop-routine-documents">
			<asp:Repeater ID="rptFiles" runat="server" OnItemDataBound="rptFiles_ItemDataBound"
				OnItemCommand="rptFiles_ItemCommand">
				<HeaderTemplate>
					<table class="table">
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td>
							<asp:Literal ID="ltFile" runat="server" />
						</td>
						<td>
							<asp:Literal ID="ltFileDate" runat="server" />
						</td>
						<td>
							<asp:ImageButton ID="imgBtnMoveUp" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
								CommandName="MoveUp" ImageUrl="~/wpc/Synologen/Images/arrow_up.png" ToolTip="Flytta upp dokumentet i listan" />
						</td>
						<td>
							<asp:ImageButton ID="imgBtnMoveDown" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
								CommandName="MoveDown" ImageUrl="~/wpc/Synologen/Images/arrow_down.png" ToolTip="Flytta ner dokumentet i listan" />
						</td>
						<td>
							<asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('Vill du verkligen ta bort dokumentet?')"
								CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' CommandName="Delete"
								Text="Ta bort" />
						</td>
					</tr>
				</ItemTemplate>
				<FooterTemplate>
					</table></FooterTemplate>
			</asp:Repeater>
		</div>
	</div>
	<div class="opq-editable-area">
		<h3 class="opq-sub-header">
			Ladda upp en ny egen rutin:
		</h3>
		<p>
			<asp:FileUpload ID="uplFileRoutine" runat="server" /></p>
	</div>
	<asp:Button ID="btnUploadRoutine" runat="server" Text="Ladda Upp" OnClick="btnUploadRoutine_Click" />
</asp:PlaceHolder>
