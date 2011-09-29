<%@ Control Language="C#" CodeBehind="EditSubscription.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions.EditSubscription" %>
<%@ Register  Src="~/Wpc/Synologen/LensSubscriptions/ErrorList.ascx" TagName="ErrorList" TagPrefix="WpcSynologen" %>
<%@ Register  Src="~/Wpc/Synologen/LensSubscriptions/TransactionsList.ascx" TagName="TransactionsList" TagPrefix="WpcSynologen" %>
<%@ Register  Src="~/Wpc/Synologen/LensSubscriptions/CreateTransaction.ascx" TagName="CreateTransaction" TagPrefix="WpcSynologen" %>
<%@ Register Src="~/Wpc/Synologen/ValidationButton.ascx" TagPrefix="WpcSynologen" TagName="ValidationButton" %>
<%if(Model.DisplayForm){%>
<div id="synologen-create-lens-subscription-edit" class="synologen-control">
<fieldset class="synologen-form">
	<legend>Redigera abonnemang för <%=Model.CustomerName %></legend>
	<p>
		<label for="<%=txtAccountNumber.ClientID%>">Kontonummer</label>
		<asp:TextBox ID="txtAccountNumber" runat="server" Text='<%#Model.AccountNumber %>' />
		<asp:RequiredFieldValidator ID="reqtxtAccountNumber" runat="server" ErrorMessage="Kontonummer måste anges" ControlToValidate="txtAccountNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator ID="regextxtAccountNumber" ValidationExpression="^[0-9]{5,12}$" runat="server" ErrorMessage="Kontonummer måste anges som heltal med 5-12 siffror" Display="Dynamic" ControlToValidate="txtAccountNumber">*</asp:RegularExpressionValidator>
	</p>
	<p>
		<label for="<%=txtClearingNumber.ClientID%>">Clearingnummer</label>
		<asp:TextBox ID="txtClearingNumber" runat="server" Text='<%#Model.ClearingNumber %>' />
		<asp:RequiredFieldValidator ID="reqtxtClearingNumber" runat="server" ErrorMessage="Clearingnummer måste anges" ControlToValidate="txtClearingNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator ID="regextxtClearingNumber" ValidationExpression="^[0-9]{4}$" runat="server" ErrorMessage="Clearingnummer måste anges som heltal med 4 siffror" Display="Dynamic" ControlToValidate="txtClearingNumber">*</asp:RegularExpressionValidator>
	</p>
	<p>
		<label for="<%=txtMonthlyAmount.ClientID%>">Månadsavgift</label>
		<asp:TextBox ID="txtMonthlyAmount" runat="server" Text='<%#Model.MonthlyAmount %>' />
		<asp:RequiredFieldValidator ID="reqtxtMonthlyAmount" runat="server" ErrorMessage="Månadsavgift måste anges" ControlToValidate="txtMonthlyAmount" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RangeValidator ID="rngtxtMonthlyAmount" runat="server" ErrorMessage="Månadsavgift måste anges som ett positivt tal med kommatecken som decimalavgränsare" ControlToValidate="txtMonthlyAmount" Display="Dynamic" MinimumValue="0" MaximumValue='99999,99' Type="Double" >*</asp:RangeValidator>
	</p>
	<p>
		<label for="<%=txtNotes.ClientID%>">Anteckningar</label>
		<asp:TextBox ID="txtNotes" TextMode="MultiLine" Text='<%#Model.Notes%>' runat="server" />
	</p>
	
	<p class="readonly-parameter"><label>Skapad</label><span><%#Model.CreatedDate %></span></p>
	<p class="readonly-parameter"><label>Status</label><span><%#Model.Status %></span></p>
	<p class="readonly-parameter"><label>Medgivande</label><span><%#Model.ConsentStatus %></span></p>
	<p class="readonly-parameter"><label>Autogiro medgivet</label><span><%#Model.ActivatedDate %></span></p>
	
	<asp:ValidationSummary ID="vldSummary" runat="server" />
	
	<div class="control-actions">
		<WpcSynologen:ValidationButton runat="server" OnValidateSuccess="btn_Save" OnEvent="Update_Form" />
		<asp:Button ID="btnStop" runat="server" Text="Stoppa abonnemang" OnClick="btn_Stop" Visible='<%#Model.StopButtonEnabled %>' />
		<asp:Button ID="btnStart" runat="server" Text="Starta abonnemang" OnClick="btn_Start" Visible='<%#Model.StartButtonEnabled %>' />

	</div>
	<div class="control-actions">
		<a href='<%=Model.ReturnUrl%>'>Tillbaka &raquo;</a>
	</div>
	
</fieldset>
<WpcSynologen:CreateTransaction ID="ctrlLensSubscriptionCreateTransaction" runat="server" />
<WpcSynologen:TransactionsList ID="ctrlLensSubscriptionTransactionsList" runat="server"  />
<WpcSynologen:ErrorList ID="ctrlLensSubscriptionErrorList" runat="server"  />
</div>
<%} %>
<%if(Model.ShopDoesNotHaveAccessGivenCustomer){%>
<p>Rättighet för att hantera given kund saknas. Var god kontakta systemadministratören.</p>
<%} %>
<%if(Model.ShopDoesNotHaveAccessToLensSubscriptions){%>
<p>Rättighet till linsbeställning kan inte medges. Var god kontakta systemadministratören.</p>
<%} %>
<%if(Model.SubscriptionDoesNotExist){%>
<p>Given linsbeställning saknas. Var god kontakta systemadministratören.</p>
<%} %>