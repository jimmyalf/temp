<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionCorrection.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.SubscriptionCorrection" %>
<div id="synologen-order-subscription-" class="synologen-control">
	<fieldset class="synologen-form">
		<legend>Saldokorrigering för konto <%=Model.SubscriptionBankAccountNumber %> för <%=Model.CustomerName %></legend>
		<p>
			<label for="<%=drpTransactionType.ClientID%>">Typ</label>
			<asp:DropDownList ID="drpTransactionType"  runat="server" DataSource='<%#Model.TransactionTypeList%>' DataTextField="Text" DataValueField="Value" />
			<asp:RequiredFieldValidator InitialValue="0" runat="server" ErrorMessage="Typ måste väljas" ControlToValidate="drpTransactionType" Display="Dynamic">*</asp:RequiredFieldValidator>
		</p>
		<p>
			<label for="<%=txtProductAmount.ClientID%>">Belopp (Produkt)</label>
			<asp:TextBox ID="txtProductAmount" runat="server" />
			<asp:RequiredFieldValidator runat="server" ErrorMessage="Belopp måste anges" ControlToValidate="txtProductAmount" Display="Dynamic">*</asp:RequiredFieldValidator>
			<asp:RangeValidator runat="server" ErrorMessage="Belopp måste anges som ett positivt tal med kommatecken som decimalavgränsare" ControlToValidate="txtProductAmount" Display="Dynamic" MinimumValue="0" MaximumValue='99999,99' Type="Double" >*</asp:RangeValidator>
		</p>
		<p>
			<label for="<%=txtFeeAmount.ClientID%>">Belopp (Arvode)</label>
			<asp:TextBox ID="txtFeeAmount" runat="server" />
			<asp:RequiredFieldValidator runat="server" ErrorMessage="Belopp måste anges" ControlToValidate="txtFeeAmount" Display="Dynamic">*</asp:RequiredFieldValidator>
			<asp:RangeValidator runat="server" ErrorMessage="Belopp måste anges som ett positivt tal med kommatecken som decimalavgränsare" ControlToValidate="txtFeeAmount" Display="Dynamic" MinimumValue="0" MaximumValue='99999,99' Type="Double" >*</asp:RangeValidator>
		</p>
		<asp:ValidationSummary runat="server" CssClass="error-list" />
		<div class="control-actions">
			<asp:Button runat="server" OnClick="Submit_Correction" Text="Spara"/>
		</div>
		<p>
			<a href="<%#Model.ReturnUrl %>">Tillbaka</a>
		</p>
	</fieldset>
</div>