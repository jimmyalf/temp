<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutogiroDetails.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.AutogiroDetails" %>

<div id="page" class="step5">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> <%#Model.CustomerName %></span>
	</header>

    <WpcSynologen:OrderMenu runat="server" />
   <div id="tab-container">
	   <fieldset class="step-5">
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
				<asp:RadioButtonList ID="rblSubscriptionTime" DataSource="<%#Model.SubscriptionOptions %>" DataTextField="DisplayName" DataValueField="Value" SelectedValue="<%#Model.SelectedSubscriptionOption.Value %>" runat="server" RepeatLayout="UnorderedList" TextAlign="Right" CssClass="radio-list" ClientIDMode="Static" AutoPostBack="True" >
				</asp:RadioButtonList>
    			<asp:TextBox ID="txtCustomNumberOfTransactions" Text="<%#Model.CustomSubscriptionTime %>" runat="server" CssClass="custom-number-of-withdrawals align-right" ClientIDMode="Static" />
				<asp:CustomValidator ID="vldCustomNumberOfWithdrawals" runat="server" ErrorMessage="Vid valfri abonnemangstid måste antal dragningar anges" ControlToValidate="rblSubscriptionTime" OnServerValidate="Validate_Custom_Subscription_Time" CssClass="error-message" ValidateEmptyText="True">&nbsp;*</asp:CustomValidator>
				<asp:RequiredFieldValidator runat="server" ErrorMessage="En abonnemangstid måste anges" ControlToValidate="rblSubscriptionTime" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
      		</div>
    		<p>
    			<label>Produktpris</label>
				<asp:TextBox ID="txtProductAmount" Text="<%#Model.ProductPrice %>" runat="server" ClientIDMode="Static" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtProductAmount" ErrorMessage="Produktbelopp måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtProductAmount" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
				<asp:RangeValidator  runat="server" MinimumValue="0" MaximumValue="49999" Type="Double" ControlToValidate="txtProductAmount" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 49999" CssClass="error-message">*</asp:RangeValidator>
    		</p>
    		<p>
    			<label>Arvode</label>
				<asp:TextBox ID="txtFeeAmount" Text="<%#Model.FeePrice %>" runat="server" ClientIDMode="Static" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtFeeAmount" ErrorMessage="Arvode måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtFeeAmount" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
				<asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="49999" Type="Double" ControlToValidate="txtFeeAmount" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 49999" CssClass="error-message">*</asp:RangeValidator>
    		</p>
    		<p>
    			<label>Totalkostnad</label>
				<input type="text" id="total-withdrawal-amount" disabled="disabled" value="<%#Model.TotalWithdrawal %>" />
    		</p>
    		<p id="calculated-montly-withdrawal">
    			<label>Månadsbelopp</label>
				<input type="text" id="montly-withdrawal-amount" disabled="disabled" value="<%#Model.Montly %>" />
    		</p>
    		<p id="custom-monthly-withdrawal-price">
    			<label>Månadsbelopp (produkt)</label>
				<asp:TextBox ID="txtCustomMonthlyPrice" runat="server" Text="<%#Model.CustomMonthlyProductAmount %>" Visible="<%#Model.IsOngoingSubscription %>" ></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomMonthlyPrice" ErrorMessage="Månadsbelopp (produkt) måste anges tillsvidare-abonnemang" Display="Dynamic" CssClass="error-message" Enabled="<%#Model.IsOngoingSubscription %>" >*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtCustomMonthlyPrice" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
				<asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="10000" Type="Double" ControlToValidate="txtCustomMonthlyPrice" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 10000" CssClass="error-message">*</asp:RangeValidator>
    		</p>
    		<p id="custom-monthly-withdrawal-fee">
    			<label>Månadsbelopp (arvode)</label>
				<asp:TextBox ID="txtCustomMonthlyFee" runat="server" Text="<%#Model.CustomMonthlyFeeAmount %>" Visible="<%#Model.IsOngoingSubscription %>" ></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomMonthlyFee" ErrorMessage="Månadsbelopp (arvode) måste anges vid tillsvidare-abonnemang" Display="Dynamic" CssClass="error-message" Enabled="<%#Model.IsOngoingSubscription %>" >*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtCustomMonthlyFee" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
				<asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="10000" Type="Double" ControlToValidate="txtCustomMonthlyFee" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 10000" CssClass="error-message">*</asp:RangeValidator>
    		</p>							

			<asp:ValidationSummary runat="server" CssClass="error-list"/>
    		<div class="next-step">
				<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CausesValidation="False" />
				<asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" OnClientClick="return confirm('Detta avbryter beställningen, är du säker på att du vill avbryta beställningen?');" />
				<asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" CssClass="submit-button" CausesValidation="True"/>
    		</div>
		</fieldset>
		<div class="message-box">
			<p>
				<strong>Tips!</strong><br/>
				<strong>Swedbank</strong> har ibland fem siffror, ex. <em>8327 – 9XXXXX</em>. Utelämna då den femte siffran, i detta fall 9.
			</p>
			<p>
				<strong>Handelsbankens</strong> clearingsnummer ska alltid börja med <em>6</em>, om du inte hittar det ange 6000.
			</p>
			<p>
				Personkonto i <strong>Nordea</strong> har clearingsnummer <em>3300</em> och kontonumret är detsamma som kundens personnummer; <em>3300 – ÅÅMMDDXXXX.</em>
			</p>
			<p>
				Personkonto i <strong>Nordea</strong> där kontonumret inte är ett personnummer, så är clearingsnumret de fyra sista i kontonumret.
			</p>
			<p>
				PlusGirokonto i <strong>Nordea</strong> har clearingsnummer <em>9960</em>.
			</p>
		</div>
		<div class="message-box">
			Brytdatum är den 15:e varje månad. Registrering till och med midnatt den <em>15:e</em> innebär dragning den <em>28:e</em> innevarande månad. Registrering efter denna tidpunkt innebär dragning den <em>28:e</em> nästkommande månad.
		</div>
    </div>
  </div>