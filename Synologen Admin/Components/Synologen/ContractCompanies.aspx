<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="ContractCompanies.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.ContractCompanies" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractCompanies-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Avtalsföretag</h2>
	        <fieldset>
		        <legend><asp:Literal id="ltHeading" runat="server" Text="Lägg till avtalsföretag"/></asp:Literal></legend>		
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
		        <%--
		        <div class="formItem rstControl">        
					<asp:PlaceHolder id="plRST" runat="server" visible="false">
					<label class="labelLong">Kostnadsställe</label>
					<asp:TextBox runat="server" ID="txtNewRST"/>&nbsp;
					<asp:Button ID="btnSaveRST" runat="server" OnClick="btnSaveRST_Click" Text="Lägg till" CssClass="btnSmall"/>       
					<asp:GridView ID="gvRST" OnRowDataBound="gvRST_RowDataBound" OnRowDeleting="gvRST_Deleting"	runat="server" DataKeyNames="cId,cConnectedOrders" SkinID="Striped" >
						<Columns>
							<asp:BoundField headerText="Kostnadsställe" DataField="cName" SortExpression="cName"/>
							<asp:BoundField headerText="Ordrar" DataField="cConnectedOrders" SortExpression="cName"/>
							<asp:ButtonField headertext="Radera" commandname="Delete"  Text="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center"/>
						</Columns>
					</asp:GridView>
					</asp:PlaceHolder>          
		        </div>			        
				--%>
		        		        	        		        	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara" CssClass="btnSmall" ValidationGroup="Error"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvContractCompanies" 
				OnRowDeleting="gvContractCompanies_Deleting"
				OnRowEditing="gvContractCompanies_Editing" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Företag" DataField="cName" SortExpression="cName"/>
                    <asp:BoundField headerText="Kontrakt" DataField="cContractName" SortExpression="cContractName"/>
                    <asp:BoundField headerText="Ort" DataField="cCity" SortExpression="cCity"/>
                    <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn"/>
		            <asp:TemplateField headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn">
			            <ItemTemplate>
                            <asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" />
			            </ItemTemplate>
		            </asp:TemplateField>
                </Columns>
            </asp:GridView>          
        </div>
      </div>
    </div>
</asp:Content>
