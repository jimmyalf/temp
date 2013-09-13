<%@ Control Language="C#" AutoEventWireup="true" Codebehind="TipAFriend.ascx.cs"
	Inherits="Spinit.Wpc.Base.Presentation.Site.TipAFriend" %>
<spinit:messagemanager id="messageManager" runat="server" uniqueclientid="Base-MessageManager-TipAFriend"
	captionelement="h4" />
<asp:ValidationSummary ID="vsTipAFriend" runat="server" CssClass="negative" ValidationGroup="TipAFriend" />
<fieldset>
	<legend>Tipsa</legend>
	<p class="inform">
		Du kan tipsa flera mottagare genom att separera e-postadresserna med semikolon.</p>
	<p class="form-item">
		<asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail">Mottagarens e-postadress:</asp:Label>
		<asp:TextBox ID="txtEmail" runat="server" ToolTip="E-postadress är obligatoriskt" ValidationGroup="TipAFriend" />
		<asp:RequiredFieldValidator CssClass="error" ID="rfvEmail" runat="server" ErrorMessage="E-postadress är obligatoriskt" ControlToValidate="txtEmail" ValidationGroup="TipAFriend" Display="None" />
		<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
			Display="None" ErrorMessage="Du måste ange en giltig e-postadress!" ValidationExpression="^(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\b([;]\s?)?)*$"
			ValidationGroup="TipAFriend"></asp:RegularExpressionValidator></p>
	<p class="form-item">
		<asp:Label ID="lblFromName" runat="server" AssociatedControlID="txtFromName">Ditt namn:</asp:Label>
		<asp:TextBox ID="txtFromName" runat="server" ToolTip="Ditt namn är obligatoriskt" ValidationGroup="TipAFriend" />
		<asp:RequiredFieldValidator CssClass="error" ID="rfvFromName" runat="server" ErrorMessage="Ditt namn är obligatoriskt" ControlToValidate="txtFromName" ValidationGroup="TipAFriend" Display="None" /></p>
	<p class="form-item full-row">
		<asp:Label ID="lblMessage" runat="server" AssociatedControlID="txtMessage">Bifoga ett meddelande:</asp:Label>
		<asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" Columns="30" />
	</p>
	<p class="form-actions">
		<asp:ImageButton ID="imgBtnTipAFriend" runat="server" ValidationGroup="TipAFriend" ImageUrl="/CommonResources/Images/Button-Send-Trans.png" AlternateText="Skicka" OnClick="btnTipAFriend_Click" />
	</p>
</fieldset>
