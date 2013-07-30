<%@ Control Language="C#" CodeBehind="SubscriptionItem.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.SubscriptionItem" %>
<div id="synologen-order-subscription-" class="synologen-control">
	<fieldset class="synologen-form">
		<legend>Delabonnemang för konto <%=Model.SubscriptionBankAccountNumber %> för <%=Model.CustomerName %></legend>
    	<p>
    		<label>Produkt</label>
			<asp:TextBox ID="txtProductAmount" Text="<%#Model.ProductPrice %>" runat="server" />
			<asp:RequiredFieldValidator runat="server" ControlToValidate="txtProductAmount" ErrorMessage="Produktbelopp måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtProductAmount" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
			<asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="49999" Type="Double" ControlToValidate="txtProductAmount" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 49999" CssClass="error-message">*</asp:RangeValidator>
    	</p>
    	<p>
    		<label>Arvode</label>
			<asp:TextBox ID="txtFeeAmount" Text="<%#Model.FeePrice %>" runat="server" />
			<asp:RequiredFieldValidator runat="server" ControlToValidate="txtFeeAmount" ErrorMessage="Arvode måste anges" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtFeeAmount" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
			<asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="49999" Type="Double" ControlToValidate="txtFeeAmount" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 49999" CssClass="error-message">*</asp:RangeValidator>
    	</p>
		<%if (Model.IsOngoing) { %>
    	<p>
    		<p>
    			<label>Månadsbelopp (pris)</label>
				<asp:TextBox ID="txtCustomMonthlyPrice" runat="server" Text="<%#Model.CustomMonthlyProductAmount %>" ></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomMonthlyPrice" ErrorMessage="Månadsbelopp (pris) måste anges vid tillsvidare-abonnemang" Display="Dynamic" CssClass="error-message" Enabled="<%#Model.IsOngoing %>">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtCustomMonthlyPrice" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
				<asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="10000" Type="Double" ControlToValidate="txtCustomMonthlyPrice" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 10000" CssClass="error-message">*</asp:RangeValidator>
    		</p>
    		<p>
    			<label>Månadsbelopp (arvode)</label>
				<asp:TextBox ID="txtCustomMonthlyFee" runat="server" Text="<%#Model.CustomMonthlyFeeAmount %>" ></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomMonthlyFee" ErrorMessage="Månadsbelopp (arvode) måste anges vid tillsvidare-abonnemang" Display="Dynamic" CssClass="error-message" Enabled="<%#Model.IsOngoing %>">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(,[0-9]+)?$" ControlToValidate="txtCustomMonthlyFee" ErrorMessage="Angivet belopp måste vara numeriskt" CssClass="error-message">*</asp:RegularExpressionValidator>
				<asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="10000" Type="Double" ControlToValidate="txtCustomMonthlyFee" ErrorMessage="Angivet belopp skall ligga i intervallet 0 - 10000" CssClass="error-message">*</asp:RangeValidator>
    		</p>	
      	</p>
		<% } else {%>
    	<p>
      		<label>Max antal dragningar</label>
    		<asp:TextBox ID="txtNumberOfWithdrawals" Text="<%#Model.WithdrawalsLimit %>" runat="server" />
			<asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+$" ControlToValidate="txtNumberOfWithdrawals" ErrorMessage="Antal dragningar måste anges som ett positivt heltal med siffror" CssClass="error-message">*</asp:RegularExpressionValidator>
			<asp:CustomValidator runat="server" ErrorMessage="Angiven abonnemangstid måste vara större eller lika med antal dragningar" ControlToValidate="txtNumberOfWithdrawals" OnServerValidate="Validate_Subscription_Time" CssClass="error-message" ValidateEmptyText="True" Enabled="<%#!Model.IsOngoing %>">&nbsp;*</asp:CustomValidator>
      	</p>
		<% } %>


		<p>
			<label>Utförda dragningar</label>
			<span><%#Model.NumerOfPerformedWithdrawals %></span>
		</p>
    	<p>
			<label>Dragningsbelopp</label>
			<span><%#Model.MonthlyWithdrawalAmount %></span>
    	</p>
		<p class="control-actions">
			<asp:Button runat="server" OnClick="Submit_SubscriptionItem" Text="Spara"/>
		</p>
		<asp:ValidationSummary runat="server" CssClass="error-list" />
		<p>
			<a href="<%#Model.ReturnUrl %>">Tillbaka</a>
		</p>
	</fieldset>
</div>
