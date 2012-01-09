<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOptions.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.PaymentOptions" %>
<div id="page" class="step4">
	<header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> <%#Model.CustomerName %></span>
	</header>
	<WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">
		<fieldset>
			<div class="progress">
				<label>Steg 4 av 6</label>
				<div id="progressbar" />
			</div>
			<p>
				<label>Välj konto för betalning</label>
				<asp:RadioButtonList runat="server" ID="rblAccounts" SelectedValue="<%# Model.SelectedOption %>" DataSource="<%# Model.Subscriptions %>" RepeatLayout="UnorderedList" DataTextField="Text" DataValueField="Value" TextAlign="Right" CssClass="radio-list" />
				<asp:RequiredFieldValidator runat="server" ErrorMessage="Ett konto måste anges" ControlToValidate="rblAccounts" Display="Dynamic" CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<asp:ValidationSummary runat="server" CssClass="error-list"/>
			<div class="next-step">
				<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CausesValidation="False" />
			    <asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" />
		        <asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" />
			</div>
		</fieldset>
	</div>
</div>