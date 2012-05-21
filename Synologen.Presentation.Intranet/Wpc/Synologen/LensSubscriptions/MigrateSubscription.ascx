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
		<p><label>Saldo</label><span><%#Model.CurrentBalance %></span></p>
		<p>
			<label>Antal påföljande dragningar</label>
			<asp:TextBox runat="server" ID="txtAdditionalWithdrawals"></asp:TextBox>
			<asp:RequiredFieldValidator runat="server" ErrorMessage="Antal påföljande dragningar måste anges" ControlToValidate="txtAdditionalWithdrawals" Display="Dynamic">*</asp:RequiredFieldValidator>
			<asp:RangeValidator runat="server" ErrorMessage="Antal påföljande dragningar måste anges som ett positivt heltal" ControlToValidate="txtAdditionalWithdrawals" Display="Dynamic" MinimumValue="0" MaximumValue='100' Type="Integer" >*</asp:RangeValidator>
		</p>
		<p>
			<label>Startsaldo</label>
			<asp:TextBox runat="server" ID="txtStartBalance">0,00</asp:TextBox>
			<asp:RequiredFieldValidator runat="server" ErrorMessage="Startsaldo måste anges" ControlToValidate="txtStartBalance" Display="Dynamic">*</asp:RequiredFieldValidator>
			<asp:RangeValidator runat="server" ErrorMessage="Saldo skall anges som ett tal i intervallet -49999 till 49999" ControlToValidate="txtStartBalance" Display="Dynamic" MinimumValue="-49999" MaximumValue="49999" Type="Double" >*</asp:RangeValidator>
		</p>
		<p class="control-actions">
			<%if(Model.IsAlreadyMigrated) {%><span>Abonnemanget har redan migrerats</span><% } %>
			<asp:Button runat="server" OnClick="Submit_Migration" Text="Migrera abonnemang" Visible="<%#!Model.IsAlreadyMigrated%>" CausesValidation="True" />
		</p>

		<asp:ValidationSummary runat="server" CssClass="error-list" />
		<p>
			<a href="<%#Model.ReturnUrl %>">Tillbaka</a>
		</p>
	</fieldset>
</div>