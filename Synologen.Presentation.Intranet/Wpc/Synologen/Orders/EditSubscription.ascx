<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditSubscription.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.EditSubscription" %>
<div class="synologen-control">
	<fieldset class="synologen-form">
		<legend>Återställ abonnemang</legend>
    	<p>
    		<label>Bankontonummer</label>
    		<asp:TextBox ID="txtBankAccountNumber" runat="server" Text='<%#Model.BankAccountNumber%>' />
			<asp:RequiredFieldValidator runat="server" ControlToValidate="txtBankAccountNumber" ErrorMessage="Bankontonummer måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
  			<asp:RegularExpressionValidator ValidationExpression="^[0-9]{5,12}$" runat="server" ErrorMessage="Kontonummer måste anges som heltal med 5-12 siffror" Display="Dynamic" ControlToValidate="txtBankAccountNumber" CssClass="error-message">*</asp:RegularExpressionValidator>
		
    	</p>
    	<p>
    		<label>Clearingnummer</label>
			<asp:TextBox ID="txtClearingNumber" runat="server" Text='<%#Model.ClearingNumber%>' />
			<asp:RequiredFieldValidator runat="server" ControlToValidate="txtClearingNumber" ErrorMessage="Clearingnummer måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator ValidationExpression="^[0-9]{4}$" runat="server" ErrorMessage="Clearingnummer måste anges som heltal med 4 siffror" Display="Dynamic" ControlToValidate="txtClearingNumber" CssClass="error-message">*</asp:RegularExpressionValidator>
    	</p>

		<asp:ValidationSummary ID="vldSummary" runat="server" CssClass="error-list" />
		<div class="control-actions">
			<asp:Button ID="btnResetSubscription" runat="server" Text="Återställ abonnemang" />
		</div>		
		<p>
			<a href="<%#Model.ReturnUrl %>">Tillbaka</a>
		</p>
	</fieldset>
</div>