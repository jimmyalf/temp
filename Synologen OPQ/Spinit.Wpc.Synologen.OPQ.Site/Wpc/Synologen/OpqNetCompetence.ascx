<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqNetCompetence.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqNetCompetence" %>
<opq:UserMessageManager ID="userMessageManager" ControlId="Opq-UserMessage-SubPage" runat="server" Visible="false" />
<h2 class="opq-sub-header">
	<asp:Literal ID="ltHeader" runat="server" />
</h2>
<div class="opq-editable-area">
	<h3 class="opq-sub-header">
		<asp:Literal ID="ltChooseUser" runat="server" />
	</h3>
	<asp:DropDownList ID="drpUsers" runat="server"/>
	<asp:Button ID="btnNavigateToNetCompetence" OnClick="BtnNavigateToNetCompetenceClick" runat="server"/>
</div>