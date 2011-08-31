<%@ Control Language="C#" CodeBehind="CreateSubscription.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions.CreateSubscription" %>
<%if(Model.DisplayForm){%>
<div id="synologen-create-lens-subscription" class="synologen-control">
<fieldset class="synologen-form">
	<legend>Skapa abonnemang f�r <%=Model.CustomerName %></legend>
	
	<p>
		<label for="<%=txtAccountNumber.ClientID%>">Kontonummer</label>
		<asp:TextBox ID="txtAccountNumber" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtAccountNumber" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="Kontonummer m�ste anges" ControlToValidate="txtAccountNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator ID="regextxtAccountNumber" ValidationGroup="vgCreateSubscription" ValidationExpression="^[0-9]{5,12}$" runat="server" ErrorMessage="Kontonummer m�ste anges som heltal med 5-12 siffror" Display="Dynamic" ControlToValidate="txtAccountNumber">*</asp:RegularExpressionValidator>
	</p>
	<p>
		<label for="<%=txtClearingNumber.ClientID%>">Clearingnummer</label>
		<asp:TextBox ID="txtClearingNumber" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtClearingNumber" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="Clearingnummer m�ste anges" ControlToValidate="txtClearingNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator ID="regextxtClearingNumber" ValidationGroup="vgCreateSubscription" ValidationExpression="^[0-9]{4}$" runat="server" ErrorMessage="Clearingnummer m�ste anges som heltal med 4 siffror" Display="Dynamic" ControlToValidate="txtClearingNumber">*</asp:RegularExpressionValidator>
	</p>
	<p>
		<label for="<%=txtMonthlyAmount.ClientID%>">M�nadsavgift</label>
		<asp:TextBox ID="txtMonthlyAmount" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtMonthlyAmount" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="M�nadsavgift m�ste anges" ControlToValidate="txtMonthlyAmount" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RangeValidator ID="rngtxtMonthlyAmount" ValidationGroup="vgCreateSubscription" runat="server" ErrorMessage="M�nadsavgift m�ste anges som ett positivt tal med kommatecken som decimalavgr�nsare" ControlToValidate="txtMonthlyAmount" Display="Dynamic" MinimumValue="0" MaximumValue='99999,99' Type="Double" >*</asp:RangeValidator>
	</p>
	<p>
		<label for="<%=txtNotes.ClientID%>">Anteckningar</label>
		<asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" />
	</p>
	<div class="control-actions">
		<asp:Button ID="btnSave" ValidationGroup="vgCreateSubscription" runat="server" Text="Spara" />
	</div>
	
	<asp:ValidationSummary ID="vldSummary" ValidationGroup="vgCreateSubscription" runat="server" />
</fieldset>
</div>
<%} %>
<%if(Model.ShopDoesNotHaveAccessGivenCustomer){%>
<p>R�ttighet f�r att hantera given kund saknas. Var god kontakta systemadministrat�ren.</p>
<%} %>
<%if(Model.ShopDoesNotHaveAccessToLensSubscriptions){%>
<p>R�ttighet till linsbest�llning kan inte medges. Var god kontakta systemadministrat�ren.</p>
<%} %>