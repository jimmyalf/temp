<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqNetCompetence.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqNetCompetence" %>
<opq:UserMessageManager ID="userMessageManager" ControlId="Opq-UserMessage-SubPage" runat="server" Visible="false" />

<div class="opq-nodes">
    <asp:RadioButtonList runat="server" ID="rblNodes" />
</div>
<h3 class="opq-sub-header">Välj användare:</h3>
<div class="opq-row">
	<div class="opq-users">
	    <asp:DropDownList ID="drpUsers" runat="server"/>
	</div>
	<br />
	<div class="netcompetence-btn">
	    <asp:Button ID="btnNavigateToNetCompetence" OnClick="BtnNavigateToNetCompetenceClick" runat="server" OnLoad="BtnLoad" />
	</div>
	<br />
</div>