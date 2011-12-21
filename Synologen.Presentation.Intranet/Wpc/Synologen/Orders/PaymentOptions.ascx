<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOptions.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.PaymentOptions" %>
<div id="page" class="step4">
	<header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> <%#Model.CustomerName %></span>
	</header>
	<WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">
		<fieldset>
			<legend/>
			<div class="progress">
				<label>Steg 4 av 6</label>
				<div id="progressbar" />
			</div>
			<asp:RadioButtonList runat="server" ID="rblAccounts" DataSource="<%# Model.Subscriptions %>" RepeatLayout="Flow" RepeatDirection="Horizontal" />
		</fieldset>
		<fieldset>
			<legend/>
			<div class="next-step">
				<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" />
			    <asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" />
		        <asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" />
			</div>
		</fieldset>
	</div>
</div>