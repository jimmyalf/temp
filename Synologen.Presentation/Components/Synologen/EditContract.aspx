<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="EditContract.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.EditContract" Title="Untitled Page" %>
<%@ Register Src="ContractSalesSubMenu.ascx" TagName="SubMenu" TagPrefix="syn" %>
<asp:Content runat="server" ContentPlaceHolderID="SubMenuPlaceHolder">
	<syn:SubMenu runat="server" />
</asp:Content>
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
					<div class="formItem clearLeft">
						<label class="labelLong">Aktiv</label>
						<asp:CheckBox id="chkActive" runat="server" Checked='<%#Contract.Active%>' />				
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Koppla avtalet till alla synologbutiker</label>
						<asp:CheckBox id="chkShopConnection" runat="server" Checked="true" />				
					</div>				
					<div class="formItem clearLeft">
						<label class="labelLong">Faktureras ej</label>
						<asp:CheckBox id="chkIsNoOp" runat="server" Checked='<%#Contract.DisableInvoice%>' />				
					</div>						
					<div class="formItem clearLeft">
						<label class="labelLong">Fakturera med brev</label>
						<asp:CheckBox id="chkForceCustomAddress" runat="server" Checked='<%#Contract.ForceCustomAddress%>' />				
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
