<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MigrateSubscription.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions.MigrateSubscription" %>
<div id="synologen-order-subscription-" class="synologen-control">
	<fieldset>
		<legend>Migrera abonnemang</legend>
		<p><label>Kund</label><span><%#Model.Customer %></span></p>
		<p><label>Kontonummer</label><span><%#Model.AccountNumber %></span></p>
		<p><label>Clearingnummer</label><span><%#Model.ClearingNumber %></span></p>
		<p><label>Status</label><span><%#Model.Status %></span></p>
		<p><label>Skapat</label><span><%#Model.CreatedDate %></span></p>
		<p><label>Antal utförda dragningar</label><span><%#Model.PerformedWithdrawals %></span></p>
		<p>
			<label>Antal påföljande dragningar</label>
			<asp:TextBox runat="server" ID="txtAdditionalWithdrawals"></asp:TextBox>
			<asp:RegularExpressionValidator runat="server" ValidationExpression="^\d+$" ControlToValidate="txtAdditionalWithdrawals" ErrorMessage="Antal dragningar måste anges som ett positivt heltal med siffror" CssClass="error-message">*</asp:RegularExpressionValidator>
		</p>
		<p class="control-actions">
			<%if(Model.IsAlreadyMigrated) {%><span>Abonnemanget har redan migrerats</span><% } %>
			<asp:Button runat="server" OnClick="Submit_Migration" Text="Migrera abonnemang" Visible="<%#!Model.IsAlreadyMigrated%>" />
		</p>

		<asp:ValidationSummary runat="server" CssClass="error-list" />
		<p>
			<a href="<%#Model.ReturnUrl %>">Tillbaka</a>
		</p>
	</fieldset>
</div>