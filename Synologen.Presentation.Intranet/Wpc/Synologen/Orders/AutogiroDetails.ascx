﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutogiroDetails.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.AutogiroDetails" %>

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
    			<asp:TextBox ID="txtBankAccountNumber" runat="server" Enabled="<%#Model.IsNewSubscription %>" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtBankAccountNumber" Enabled="<%#Model.IsNewSubscription %>" ErrorMessage="Bankontonummer måste anges" Display="Dynamic">*</asp:RequiredFieldValidator>
    		</p>
    		<p>
    			<label>Clearingnummer</label>
				<asp:TextBox ID="txtClearingNumber" runat="server" Enabled="<%#Model.IsNewSubscription %>" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtClearingNumber" Enabled="<%#Model.IsNewSubscription %>" ErrorMessage="Clearingnummer måste anges" Display="Dynamic">*</asp:RequiredFieldValidator>
    		</p>
    		<div>
      			<label>Abonnemangstid</label>
				<asp:RadioButtonList ID="rblSubscriptionTime" runat="server" RepeatLayout="UnorderedList" >
					<asp:ListItem Text="3 månader" Value="3" class="3-withdrawals" />
					<asp:ListItem Text="6 månader" Value="6" class="6-withdrawals" />
					<asp:ListItem Text="12 månader" Value="12" class="12-withdrawals" />
					<asp:ListItem Text="Löpande" Value="<%=UseContinousWithdrawals %>" class="continuous-withdrawals"/>
					<asp:ListItem Text="Valfritt" Value="<%=UseCustomNumberOfWithdrawals %>" class="custom-number-of-withdrawals" />
				</asp:RadioButtonList>
    			<asp:TextBox ID="txtCustomNumberOfTransactions" runat="server" CssClass="custom-number-of-withdrawals" />
      		</div>
    	
    		<p>
    			<label>Momsbelopp</label>
				<asp:TextBox ID="txtVATAmount" runat="server" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtVATAmount" ErrorMessage="Momsbelopp måste anges" Display="Dynamic">*</asp:RequiredFieldValidator>
    		</p>
    		<p>
    			<label>Momsfritt belopp</label>
				<asp:TextBox ID="txtVatFreeAmount" runat="server" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtVatFreeAmount" ErrorMessage="Momsfritt belopp måste anges" Display="Dynamic">*</asp:RequiredFieldValidator>
    		</p>
    		<p>
    			<label>Vald artikel</label>
				<span><%=Model.SelectedArticleName%></span>
    		</p>
			<%if(Model.EnableAutoWithdrawal){ %>
    		<p>
    			<label>Totaluttag</label>
				<asp:TextBox ID="txtTotalWithdrawalAmount" runat="server" />
				<asp:RequiredFieldValidator runat="server" ControlToValidate="txtTotalWithdrawalAmount" ErrorMessage="Totaluttag måste anges" Display="Dynamic">*</asp:RequiredFieldValidator>
    		</p>
			<% } %>
			<asp:ValidationSummary runat="server"/>
    		<div class="next-step">
				<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" />
				<asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" />
				<asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" />
    		</div>
		</fieldset>
    </div>
  </div>