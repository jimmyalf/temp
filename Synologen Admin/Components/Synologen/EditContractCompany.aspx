<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="EditContractCompany.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.EditContractCompany" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-EditContractCompany-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Redigera avtalsföretag</h2>
	        <fieldset>
		        <legend>Redigera avtalsföretag</legend>		
		        <div class="formItem clearLeft">
					<div class="formItem">
						<label class="labelLong">Avtalskund *</label>
						<asp:DropDownList ID="drpContracts" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Företagsnamn *</label>
						<asp:TextBox runat="server" ID="txtName"/>
						<asp:RequiredFieldValidator id="rfvName" runat="server" errormessage="Företagsnamn saknas." controltovalidate="txtName" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Företagskod i SPCS *</label>
						<asp:TextBox runat="server" ID="txtCompanyCode"/>
						<asp:RequiredFieldValidator id="rfvCompanyCode" runat="server" errormessage="Företagskod saknas." controltovalidate="txtCompanyCode" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Bank ID *</label>
						<asp:TextBox runat="server" ID="txtBankIDCode"/>
						<asp:RequiredFieldValidator id="rfvBankIDCode" runat="server" errormessage="Bank-ID saknas." controltovalidate="txtBankIDCode" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>
						<asp:RangeValidator ID="vldBankIDCode" runat="server"  ErrorMessage="Bank-ID felaktigt. (Skall vara 4 siffror)" Type="Integer" MinimumValue="1000" MaximumValue="9999" ControlToValidate="txtBankIDCode" Display="Dynamic" ValidationGroup="Error">*</asp:RangeValidator>
					</div>		 		        
					<div class="formItem clearLeft">
						<label class="labelLong">Adress 1</label>
						<asp:TextBox runat="server" ID="txtAddress"/>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Adress 2 *</label>
						<asp:TextBox runat="server" ID="txtAddress2"/>
						<asp:RequiredFieldValidator id="rfvAddress2" runat="server" errormessage="Adress 2 saknas." controltovalidate="txtAddress2" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>		            
					</div>	
					<div class="formItem clearLeft">
						<label class="labelLong">Postnummer *</label>
						<asp:TextBox runat="server" ID="txtZip"/>
						<asp:RequiredFieldValidator id="rfvZip" runat="server" errormessage="Postnummer saknas." controltovalidate="txtZip" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>		            
					</div>	
					<div class="formItem clearLeft">
						<label class="labelLong">Ort *</label>
						<asp:TextBox runat="server" ID="txtCity"/>
						<asp:RequiredFieldValidator id="rfvCity" runat="server" errormessage="Ort saknas." controltovalidate="txtCity" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>		            
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Aktivt</label><br />
						<asp:CheckBox id="chkActive" runat="server" />
					</div>
		        </div>	
		        <div class="formItem">
					<div class="formItem clearLeft">
		        		<label class="labelLong">Org Nr</label><br />
						<asp:TextBox id="txtOrganizationNumber" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Adresskod</label><br />
						<asp:TextBox id="txtAddressCode" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Momsredovisningsnummer</label><br />
						<asp:TextBox id="txtTaxAccountingCode" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Faktureringsperiod (antal dagar) *</label><br />
						<asp:TextBox id="txtPaymentDuePeriod" runat="server" />
						<asp:RequiredFieldValidator id="reqPaymentDueDate"  runat="server" errormessage="Faktureringsperiod saknas" controltovalidate="txtPaymentDuePeriod" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>       
						<asp:RangeValidator ID="vldPaymentDueDate" runat="server" ErrorMessage="Faktureringsperiod felaktig. (Skall anges i siffror)" Type="Integer" MinimumValue="1" MaximumValue="120" ControlToValidate="txtPaymentDuePeriod" Display="Dynamic" ValidationGroup="Error">*</asp:RangeValidator>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">EDI MottagarID</label><br />
						<asp:TextBox id="txtEDIRecipientId" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Faktureringsmetod *</label><br />
						<asp:DropDownList id="drpInvoicingMethods" runat="server" />
						<asp:RequiredFieldValidator id="reqInvoicingMethods" InitialValue="0" runat="server" errormessage="Faktureringsmetod saknas" controltovalidate="drpInvoicingMethods" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>       
					</div>
		        </div>
		        		        	        		        	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara" CssClass="btnSmall" ValidationGroup="Error" CausesValidation="true"/>
		        </div>
	        </fieldset>       
        </div>
      </div>
    </div>
</asp:Content>
