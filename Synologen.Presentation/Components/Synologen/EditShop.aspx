﻿<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="EditShop.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.EditShop" Title="Untitled Page" %>
<%@ Register Src="SynologenSubMenu.ascx" TagName="SubMenu" TagPrefix="syn" %>
<asp:Content runat="server" ContentPlaceHolderID="SubMenuPlaceHolder">
	<syn:SubMenu runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
    <div id="dCompMain" class="Components-Synologen-EditShop-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Butik</h1>
                <fieldset><legend>Butikdetaljer</legend>
					<div class="formItem">
						<label class="labelLong">Butik</label>
						<asp:TextBox id="txtShopName" runat="server" />
					</div>
					<div class="formItem">
						<label class="labelLong">Butikgrupp</label>
						<asp:DropDownList id="drpShopGroups" runat="server" DataValueField="Id" DataTextField="Name"/>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Organisationsnummer</label>
						<asp:TextBox id="txtOrganizationNumber" runat="server" />
						<asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrganizationNumber" ErrorMessage="Organisationsnummer måste anges" Display="dynamic" >*</asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator runat="server" ValidationExpression="^\d{6}[-\+]{1}\d{4}$" ControlToValidate="txtOrganizationNumber" ErrorMessage="Organisationsnummer måste anges på giltigt format NNNNNN-NNNN" Display="dynamic">*</asp:RegularExpressionValidator>
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Butiknummer</label>
						<asp:TextBox id="txtShopNumber" runat="server" />
					</div>					
					<div class="formItem clearLeft">
						<label class="labelLong">Beskrivning</label>
						<asp:TextBox id="txtShopDescription" runat="server" cssClass="txtAreaWide" TextMode="MultiLine" />
					</div>	
					
					<div class="formItem clearLeft">
						<div class="formItem"><label class="labelLong">Postbox/Företagsnamn</label>
						<asp:TextBox id="txtAddress" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Gatuadress</label>						
						<asp:TextBox id="txtAddress2" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Postnummer</label>
						<asp:TextBox id="txtZip" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Stad</label>
						<asp:TextBox id="txtCity" runat="server" /></div>			
						<div class="formItem"><label class="labelLong">Hemsida</label>
						<asp:TextBox id="txtUrl" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Karta</label>
						<asp:TextBox id="txtMapUrl" runat="server" /></div>																	
					</div>				
					<div class="formItem">
						<div class="formItem"><label class="labelLong">Telefon 1</label>
						<asp:TextBox id="txtPhone" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Telefon 2</label>						
						<asp:TextBox id="txtPhone2" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Fax</label>
						<asp:TextBox id="txtFax" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Epost</label>
						<asp:TextBox id="txtEmail" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Kontaktperson förnamn</label>
						<asp:TextBox id="txtContactFirstName" runat="server" /></div>
						<div class="formItem"><label class="labelLong">Kontaktperson efternamn</label>
						<asp:TextBox id="txtContactLastName" runat="server" /></div>																		
					</div>
                    <div class="formItem clearLeft">    
                        <label for="<%= btnFetchCoordinates.ClientID %>">Hämta koordinater för angiven adress</label>
                        <asp:Button ID="btnFetchCoordinates" runat="server" Text="Hämta koordinater" OnClick="btnFetchCoordinates_OnClick" />
                    </div>
                    <div class="formItem infoItem">
                        <p>För att butiken ska visas på karta, måste man antingen hämta eller ange koordinater för den.</p>
                    </div>
                    <div class="formItem clearLeft">
                        <label class="labelLong">Latitud</label>
						<asp:TextBox id="txtLatitude" runat="server" />
                    </div>
                    <div class="formItem">
                        <label class="labelLong">Longitud</label>
						<asp:TextBox id="txtLongitude" runat="server" />
                    </div>
                    <div class="formItem clearLeft">
                        <asp:PlaceHolder ID="phLinkToMap" runat="server">
                            <button disabled="disabled">Visa koordinater</button>
                        </asp:PlaceHolder>
                    </div>
                    <div class="formItem infoItem">
                        <p>
                            När du väl hämtat rätt koordinater, kan du kontrollera dessa genom länken till vänster.
                            Du får upp en karta vilken innehåller en grön pil, där den gröna pilen motsvarar dina koordinater.
                        </p>
                    </div>
					<div class="formItem clearLeft">
						<label class="labelLong">Giro-typ</label>
						<asp:DropDownList id="drpGiroType" runat="server" DataValueField="cId" DataTextField="cName" />				
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Giro-Nummer</label>
						<asp:TextBox id="txtGiroNumber" runat="server" />				
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Bank</label>
						<asp:TextBox id="txtGiroSupplier" runat="server" />				
					</div>													
					<div class="formItem clearLeft">
						<label class="labelLong">Aktiv</label>
						<asp:CheckBox id="chkActive" runat="server" />				
					</div>
					<div class="formItem clearLeft">
                        <label class="labelLong">Butiktyp</label>
                        <asp:RadioButtonList ID="rdblCategories" runat="server" DataValueField="cId" DataTextField="cName" RepeatColumns="2" RepeatLayout="Table" AutoPostBack="true" />
					</div>					
					<div class="formItem clearLeft">
						<label class="labelLong">Avtalskunder * </label>
						<asp:CheckBoxList id="chkContractCustomers" runat="server" DataValueField="cId" DataTextField="cName" RepeatColumns="2" RepeatLayout="Table"  />
					</div>
					<div class="formItem infoItem">
						<span>*</span><p>Synologer får automatiskt alla avtalskunder oavsett om de är ikryssade eller ej.</p>
					</div>		
					<div class="formItem clearLeft">
						<label class="labelLong">Utrustning</label>
						<asp:CheckBoxList id="chkEquipment" runat="server" DataValueField="cId" DataTextField="cName" RepeatColumns="2" RepeatLayout="Table" />
					</div>
					<div class="formItem clearLeft">
						<label class="labelLong">Rättigheter</label>
						<asp:CheckBoxList id="chkShopAccess" runat="server" DataValueField="Value" DataTextField="Text" RepeatColumns="2" RepeatLayout="Table" />
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
