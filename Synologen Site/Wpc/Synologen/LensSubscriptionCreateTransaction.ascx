<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LensSubscriptionCreateTransaction.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptionCreateTransaction" %>
<fieldset class="synologen-form">
	<legend>Ny transaktion</legend>
	<% if (Model.DisplayChooseReason) { %>
	<div class="control-actions">
		<asp:Button ID="btnWithdrawal" ValidationGroup="vgCreateTransaction" runat="server" Text="Uttag" />
		<asp:Button ID="btnCorrection" ValidationGroup="vgCreateTransaction" runat="server" Text="Korrigering" />
	</div>
	<% } %>
	<% if (Model.DisplaySaveCorrection) { %>
	<label for="<%=drpTransactionType.ClientID%>">Typ</label>
	<asp:DropDownList ID="drpTransactionType"  runat="server" DataSource='<%#Model.TypeList%>' DataTextField="Text" DataValueField="Value" />
	<asp:RequiredFieldValidator ID="reqdrpTransactionType" InitialValue="0" ValidationGroup="vgCreateTransaction" runat="server" ErrorMessage="Typ måste väljas" ControlToValidate="drpTransactionType" Display="Dynamic">*</asp:RequiredFieldValidator>
	
	<% } %>
	<% if (Model.DisplaySaveCorrection || Model.DisplaySaveWithdrawal) { %>	
	<label for="<%=txtAmount.ClientID%>">Belopp</label>
	<asp:TextBox ID="txtAmount" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtAmount" ValidationGroup="vgCreateTransaction" runat="server" ErrorMessage="Belopp måste anges" ControlToValidate="txtAmount" Display="Dynamic">*</asp:RequiredFieldValidator>
	<asp:RangeValidator ID="rngtxtAmount" ValidationGroup="vgCreateTransaction" runat="server" ErrorMessage="Belopp måste anges som ett positivt tal med kommatecken som decimalavgränsare" ControlToValidate="txtAmount" Display="Dynamic" MinimumValue="0" MaximumValue='99999,99' Type="Double" >*</asp:RangeValidator>
	<% } %>
	
	<% if (Model.DisplaySaveWithdrawal || Model.DisplaySaveCorrection) { %>
	<div class="control-actions">
		<asp:Button ID="btnSave" ValidationGroup="vgCreateTransaction" runat="server" Text="Spara" />
		<asp:Button ID="btnCancel" runat="server" Text="Avbryt" />
	</div>
	
	<asp:ValidationSummary ID="vldSummary" ValidationGroup="vgCreateTransaction" runat="server" />
	<% } %>

</fieldset>