<%@ Control Language="C#" CodeBehind="LensSubscriptionCreateSubscription.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptionCreateSubscription" %>
<%if(Model.DisplayForm){%>
<div id="synologen-create-lens-subscription" class="synologen-control">
<fieldset class="synologen-form">
	<legend>Skapa abonnemang för <%=Model.CustomerName %></legend>
	
	<label for="<%=txtAccountNumber.ClientID%>">Kontonummer</label>
	<asp:TextBox ID="txtAccountNumber" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtAccountNumber" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="Kontonummer måste anges" ControlToValidate="txtAccountNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
	<asp:RegularExpressionValidator ID="regextxtAccountNumber" ValidationGroup="vgCreateSubscription" ValidationExpression="^[0-9]{5,12}$" runat="server" ErrorMessage="Kontonummer måste anges som heltal med 5-12 siffror" Display="Dynamic" ControlToValidate="txtAccountNumber">*</asp:RegularExpressionValidator>
	
	<label for="<%=txtClearingNumber.ClientID%>">Clearingnummer</label>
	<asp:TextBox ID="txtClearingNumber" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtClearingNumber" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="Clearingnummer måste anges" ControlToValidate="txtClearingNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
	<asp:RegularExpressionValidator ID="regextxtClearingNumber" ValidationGroup="vgCreateSubscription" ValidationExpression="^[0-9]{4}$" runat="server" ErrorMessage="Clearingnummer måste anges som heltal med 4 siffror" Display="Dynamic" ControlToValidate="txtClearingNumber">*</asp:RegularExpressionValidator>
	
	<label for="<%=txtMonthlyAmount.ClientID%>">Månadsavgift</label>
	<asp:TextBox ID="txtMonthlyAmount" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtMonthlyAmount" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="Månadsavgift måste anges" ControlToValidate="txtMonthlyAmount" Display="Dynamic">*</asp:RequiredFieldValidator>
	<asp:RangeValidator ID="rngtxtMonthlyAmount" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="Månadsavgift måste anges som ett positivt tal med kommatecken som decimalavgränsare" ControlToValidate="txtMonthlyAmount" Display="Dynamic" MinimumValue="0" MaximumValue='99999,99' Type="Double" >*</asp:RangeValidator>
	
	<asp:Button ID="btnSave" ValidationGroup="vgCreateSubscription" runat="server" Text="Spara" />
	
	<asp:ValidationSummary ID="vldSummary" runat="server" />
</fieldset>
</div>
<%} %>
<%if(Model.ShopDoesNotHaveAccessGivenCustomer){%>
<p>Rättighet för att hantera given kund saknas. Var god kontakta systemadministratören.</p>
<%} %>
<%if(Model.ShopDoesNotHaveAccessToLensSubscriptions){%>
<p>Rättighet till linsbeställning kan inte medges. Var god kontakta systemadministratören.</p>
<%} %>