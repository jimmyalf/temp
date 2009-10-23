<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="EditContract.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.EditContract" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
    <div id="dCompMain" class="Components-Synologen-EditContract-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Avtalskund</h1>
                <fieldset><legend>Företagsinformation</legend>
					<div class="formItem">
						<label class="labelLong">Företag</label>
						<asp:TextBox id="txtContractCustomerName" runat="server" Text='<%#Contract.Name%>' />
					</div>
					<div style="visibility:hidden; display:none;">
						<div class="formItem clearLeft">
							<label class="labelLong">Kod</label>
							<asp:TextBox id="txtContractCustomerCode" runat="server" Text='<%#Contract.Code%>' />
						</div>					
						<div class="formItem clearLeft">
							<label class="labelLong">Beskrivning</label>
							<asp:TextBox id="txtContractCustomerDescription" runat="server" CssClass="txtAreaWide" TextMode="MultiLine" Text='<%#Contract.Description%>' />
						</div>	
						
						<div class="formItem clearLeft">
							<div class="formItem"><label class="labelLong">Address 1</label>
							<asp:TextBox id="txtAddress" runat="server" Text='<%#Contract.Address%>' /></div>
							<div class="formItem"><label class="labelLong">Address 2</label>						
							<asp:TextBox id="txtAddress2" runat="server" Text='<%#Contract.Address2%>' /></div>
							<div class="formItem"><label class="labelLong">Postnummer</label>
							<asp:TextBox id="txtZip" runat="server" Text='<%#Contract.Zip%>' /></div>
							<div class="formItem"><label class="labelLong">Stad</label>
							<asp:TextBox id="txtCity" runat="server" Text='<%#Contract.City%>' /></div>													
						</div>				
						<div class="formItem">
							<div class="formItem"><label class="labelLong">Telefon 1</label>
							<asp:TextBox id="txtPhone" runat="server" Text='<%#Contract.Phone%>' /></div>
							<div class="formItem"><label class="labelLong">Telefon 2</label>						
							<asp:TextBox id="txtPhone2" runat="server" Text='<%#Contract.Phone2%>' /></div>
							<div class="formItem"><label class="labelLong">Fax</label>
							<asp:TextBox id="txtFax" runat="server" Text='<%#Contract.Fax%>' /></div>
							<div class="formItem"><label class="labelLong">Epost</label>
							<asp:TextBox id="txtEmail" runat="server"  Text='<%#Contract.Email%>'/></div>
						</div>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Aktiv</label>
						<asp:CheckBox id="chkActive" runat="server" Checked='<%#Contract.Active%>' />				
					</div>
					<div class="formItem infoItem">
						<span>!</span><p>Nya avtal kopplas automatiskt till alla synologbutiker.</p>
					</div>											
                </fieldset>			
				                	
				<fieldset>													
					<div class="formCommands">		
						<input type="button" name="inputBack" class="btnBig" onclick="javascript:window.history.back();" value="Tillbaka" />
					    <asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Spara" SkinId="Big" CausesValidation="true"/>
					</div>
					<asp:ValidationSummary ID="vldsUser" runat="server" ShowMessageBox="true" ShowSummary="false" />
				</fieldset>
			</div>
        </div>
    </div>
</asp:Content>
