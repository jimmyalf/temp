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
						<label class="labelLong">Bank ID</label>
						<asp:TextBox runat="server" ID="txtBankIDCode"/>
					</div>		 		        
					<div class="formItem clearLeft">
						<label class="labelLong">Postbox</label>
						<asp:TextBox runat="server" ID="txtAddress"/>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Gatuadress/Företagsnamn</label>
						<asp:TextBox runat="server" ID="txtAddress2"/>
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
						<label class="labelLong">Land *</label>
						<asp:DropDownList ID="drpCountry" runat="server" DataValueField="Id" DataTextField="Name" />
					</div>					
					<div class="formItem clearLeft">
						<label class="labelLong">Aktivt</label><br />
						<asp:CheckBox id="chkActive" runat="server" />
					</div>							
		        </div>	
		        <div class="formItem">
					<div class="formItem clearLeft">
		        		<label class="labelLong">Org Nr</label>
						<asp:TextBox id="txtOrganizationNumber" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Faktureringsnamn *</label>
						<asp:TextBox id="txtInvoiceCompanyName" runat="server" />
						<asp:RequiredFieldValidator id="reqInvoiceCompanyName"  runat="server" errormessage="Företagets faktureringsnamn saknas" controltovalidate="txtInvoiceCompanyName" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Momsredovisningsnummer</label>
						<asp:TextBox id="txtTaxAccountingCode" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Faktureringsperiod (antal dagar) *</label>
						<asp:TextBox id="txtPaymentDuePeriod" runat="server" />
						<asp:RequiredFieldValidator id="reqPaymentDueDate"  runat="server" errormessage="Faktureringsperiod saknas" controltovalidate="txtPaymentDuePeriod" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>       
						<asp:RangeValidator ID="vldPaymentDueDate" runat="server" ErrorMessage="Faktureringsperiod felaktig. (Skall anges i siffror)" Type="Integer" MinimumValue="1" MaximumValue="120" ControlToValidate="txtPaymentDuePeriod" Display="Dynamic" ValidationGroup="Error">*</asp:RangeValidator>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">EDI MottagarID</label>
						<asp:TextBox id="txtEDIRecipientId" runat="server" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Faktureringsmetod *</label>
						<asp:DropDownList id="drpInvoicingMethods" runat="server" />
						<asp:RequiredFieldValidator id="reqInvoicingMethods" InitialValue="0" runat="server" errormessage="Faktureringsmetod saknas" controltovalidate="drpInvoicingMethods" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>
					</div>
					<div class="formItem clearLeft" id="invoice-free-text-form-item">
						<label class="labelLong">Fritextmall för fakturering</label>
						<asp:TextBox id="txtInvoiceFreeTextTemplate" runat="server" TextMode="MultiLine" CssClass="txtAreaWide" />
						<div class="form-item-help" title="Tillgängliga fält" >
							<ul>
								<li>{CustomerFirstName}</li>
								<li>{CustomerLastName}</li>
								<li>{CustomerName}</li>
								<li>{CustomerPersonalIdNumber}</li>
								<li>{CustomerPersonalBirthDateString}</li>
								<li>{RST}</li>
								<li>{CompanyUnit}</li>
								<li>{BuyerCompanyId}</li>
								<li>{BankCode}</li>
								<li>{SellingShopName}</li>
								<li>{SellingShopNumber}</li>
								<li>{SellingShopOrgNumber}</li>
							</ul>
						</div>
					</div>						
		        </div>     
				<div class="formItem clearLeft">
					<label class="labelLong">Valideringsregler</label><br />
					<asp:CheckBoxList id="chkValidationRules" runat="server" DataValueField="cId" DataTextField="cNameAndDescription" RepeatColumns="2" />
				</div>			        
		        		        	        		        	        
		        <div class="formCommands">
					<input type="button" name="inputBack" class="btnBig" onclick="window.location='ContractCompanies.aspx?id=<%=SelectedContractID %>'" value="Tillbaka" />
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara" SkinId="Big" ValidationGroup="Error" CausesValidation="true"/>
		        </div>
	        </fieldset>       
		</div>
	</div>
</div>
</asp:Content>
