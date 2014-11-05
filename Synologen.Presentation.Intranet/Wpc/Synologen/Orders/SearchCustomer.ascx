<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchCustomer.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.SearchCustomer" %>
<div id="page" class="step1">
    <div>
		<h1>Linsabonnemang</h1>
	</div>

	<WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">

   	<fieldset>
   		<div class="progress">
   	    	<label>Steg 1 av 6</label>
			<div id="progressbar"></div>
   	    </div>
    	<p>
            <label for="<%=txtPersonalIdNumber.ClientID%>">Personnummer</label>
            <span>
				<asp:TextBox ID="txtPersonalIdNumber" runat="server" placeholder="ÅÅÅÅMMDDNNNN" />
				<asp:RequiredFieldValidator ID="reqtxtPersonalIdNumber" runat="server" ErrorMessage="Personnummer måste anges" ControlToValidate="txtPersonalIdNumber" Display="Dynamic" CssClass="error-message">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="regextxtPersonalIdNumber" ValidationExpression="^(19|20)(\d){2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{4}$" runat="server" ErrorMessage="Personnummer måste anges som ÅÅÅÅMMDDXXXX" Display="Dynamic" ControlToValidate="txtPersonalIdNumber" CssClass="error-message">*</asp:RegularExpressionValidator>
            </span>
        </p>

        <asp:ValidationSummary ID="vldSummary" runat="server" CssClass="error-list" />
    	<div class="next-step">
            <div class="control-actions">
                <asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" />
		        <asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" CssClass="submit-button" CausesValidation="true" />
	        </div>
        </div>
    </fieldset>
  </div>
  </div>