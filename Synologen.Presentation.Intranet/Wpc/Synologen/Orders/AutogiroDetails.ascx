<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutogiroDetails.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.AutogiroDetails" %>

<div id="page" class="step5">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> <%#Model.CustomerName %></span>
	</header>

    <WpcSynologen:OrderMenu runat="server" />
   <div id="tab-container">
	   <fieldset>
			<div class="progress">
   				<label>Steg 5 av 6</label>
	 			<div id="progressbar"></div>
   			</div>
    		<p>
    			<label>Bankontonummer</label>
    			<asp:TextBox ID="txtBankAccountNumber" runat="server" Enabled="<%#Model.IsNewSubscription %>" Text='<%#Model.BankAccountNumber%>' />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtBankAccountNumber" Enabled="<%#Model.IsNewSubscription %>" ErrorMessage="Bankontonummer måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
  				<asp:RegularExpressionValidator ID="regextxtAccountNumber" ValidationExpression="^[0-9]{5,12}$" runat="server" ErrorMessage="Kontonummer måste anges som heltal med 5-12 siffror" Display="Dynamic" ControlToValidate="txtBankAccountNumber" CssClass="error-message">*</asp:RegularExpressionValidator>
		
    		</p>
    		<p>
    			<label>Clearingnummer</label>
				<asp:TextBox ID="txtClearingNumber" runat="server" Enabled="<%#Model.IsNewSubscription %>" Text='<%#Model.ClearingNumber%>' />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtClearingNumber" Enabled="<%#Model.IsNewSubscription %>" ErrorMessage="Clearingnummer måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="regextxtClearingNumber" ValidationExpression="^[0-9]{4}$" runat="server" ErrorMessage="Clearingnummer måste anges som heltal med 4 siffror" Display="Dynamic" ControlToValidate="txtClearingNumber" CssClass="error-message">*</asp:RegularExpressionValidator>
    		</p>
    		<div>
      			<label>Abonnemangstid</label>
				<asp:RadioButtonList ID="rblSubscriptionTime" DataSource="<%#Model.SubscriptionOptions %>" DataTextField="Text" DataValueField="Value" SelectedValue="<%#Model.SelectedSubscriptionOption %>" runat="server" RepeatLayout="UnorderedList" TextAlign="Right" CssClass="radio-list" ClientIDMode="Static" >
				</asp:RadioButtonList>
    			<asp:TextBox ID="txtCustomNumberOfTransactions" Text="<%#Model.CustomSubscriptionTime %>" runat="server" CssClass="custom-number-of-withdrawals align-right" ClientIDMode="Static" />
				<asp:CustomValidator ID="vldCustomNumberOfWithdrawals" runat="server" ErrorMessage="Vid valfri abonnemangstid måste antal dragningar anges" ControlToValidate="rblSubscriptionTime" OnServerValidate="Validate_Custom_Subscription_Time" CssClass="error-message" ValidateEmptyText="True">&nbsp;*</asp:CustomValidator>
				<asp:RequiredFieldValidator runat="server" ErrorMessage="En abonnemangstid måste anges" ControlToValidate="rblSubscriptionTime" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
      		</div>
    		<p>
    			<label>Produkt</label>
				<asp:TextBox ID="txtProductAmount" Text="<%#Model.ProductPrice %>" runat="server" ClientIDMode="Static" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtProductAmount" ErrorMessage="Produktbelopp måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtProductAmount" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
    		</p>
    		<p>
    			<label>Arvode</label>
				<asp:TextBox ID="txtFeeAmount" Text="<%#Model.FeePrice %>" runat="server" ClientIDMode="Static" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtFeeAmount" ErrorMessage="Arvode måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtFeeAmount" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
    		</p>
    		<p>
    			<label>Vald artikel</label>
				<span><%=Model.SelectedArticleName%></span>
    		</p>
    		<p>
    			<label>Totalkostnad</label>
				<input type="text" id="total-withdrawal-amount" disabled="disabled" />
    		</p>
    		<p>
    			<label>Månadsbelopp</label>
				<input type="text" id="montly-withdrawal-amount" disabled="disabled" />
    		</p>
			<asp:ValidationSummary runat="server" CssClass="error-list"/>
    		<div class="next-step">
				<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CausesValidation="False" />
				<asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" OnClientClick="return confirm('Detta avbryter beställningen, är du säker på att du vill avbryta beställningen?');" />
				<asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" CausesValidation="True"/>
    		</div>
		</fieldset>
    </div>
  </div>