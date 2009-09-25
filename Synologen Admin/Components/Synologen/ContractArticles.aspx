<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="ContractArticles.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.ContractArticles" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractArticles-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Artiklar</h2>
            
            <asp:PlaceHolder id="plFilterByContract" runat="server">        	                	                  	                 	                
			<fieldset><legend>Filter</legend>
				<div class="formItem clearLeft">
					<label >Visar Företag för Avtalskund:</label>&nbsp;<span><%=SelectedContract.Name %></span><br /><br />
					<%--<input type="button" name="inputNew" class="btnBig" onclick="window.location='EditContractCompany.aspx?contractId=<%=SelectedContract.Id %>'" value="Skapa nytt avtalsföretag" />--%>
					<input type="button" name="inputBack" class="btnBig" onclick="window.location='Contracts.aspx'" value="Tillbaka" />
				</div> 	   	                                                     
			</fieldset>
            </asp:PlaceHolder>	
            
	        <fieldset>
		        <legend><asp:Literal id="ltHeading" runat="server" Text="Lägg till artikel"/></asp:Literal></legend>		
		        <div class="formItem">
		            <label class="labelLong">Avtalskund *</label>
		            <asp:DropDownList ID="drpContracts" runat="server" />
					<asp:RequiredFieldValidator id="reqContracts" InitialValue="0" runat="server" errormessage="Avtal saknas." controltovalidate="drpContracts" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>		            
		        </div>
		        <div class="formItem">
		            <label class="labelLong">Artikel *</label>
		            <asp:DropDownList ID="drpArticles" runat="server" />
		            <asp:RequiredFieldValidator id="reqArticles" InitialValue="0" runat="server" errormessage="Artikel saknas." controltovalidate="drpArticles" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>
		        </div>
		        <div class="formItem clearLeft">
		            <label class="labelLong">Pris *</label>
		            <asp:TextBox runat="server" ID="txtPrice"/>
		            <asp:RangeValidator id="vldPrice" runat="server" errormessage="Pris saknas." controltovalidate="txtPrice" Display="Dynamic" Type="Double" ValidationGroup="Error" >*</asp:RangeValidator>
		        </div>
		        <div class="formItem clearLeft">
		            <label class="labelLong">Momsfri artikel</label>
		            <asp:CheckBox runat="server" ID="chkNoVAT"/>
		        </div>
		        <div class="formItem">
		            <label class="labelLong">SPCS Kontonummer *</label>
		            <asp:TextBox id="txtSPCSAccountNumber" runat="server" />
					<asp:RequiredFieldValidator id="reqSPCSAccountNumber" runat="server" errormessage="SPCS Kontonummer saknas." controltovalidate="txtSPCSAccountNumber" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>		            
		        </div>
		        <div class="formItem clearLeft">
		            <label class="labelLong">Aktiv</label>
		            <asp:CheckBox runat="server" ID="chkActive"/>
		        </div>		        	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara" CssClass="btnSmall" ValidationGroup="Error"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvContractCustomerArticles" 
				OnRowDeleting="gvContractCustomerArticles_Deleting"
				OnRowEditing="gvContractCustomerArticles_Editing" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Namn" DataField="cName" SortExpression="cName"/>
                    <asp:BoundField headerText="Kontrakt" DataField="cContractName" SortExpression="cContractName"/>
                    <asp:BoundField headerText="Pris" DataField="cPrice" SortExpression="cPrice"/>
                    <asp:BoundField headerText="SPCS Konto" DataField="cSPCSAccountNumber" SortExpression="cSPCSAccountNumber"/>
                    <asp:TemplateField headertext="Momsfri" SortExpression="cVATFree"  HeaderStyle-CssClass="controlColumn" >
						<ItemStyle CssClass="center" />
						<ItemTemplate>
							<asp:Image id="imgVATFree" runat="server" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField headertext="Aktiv" SortExpression="cActive"  HeaderStyle-CssClass="controlColumn" >
						<ItemStyle CssClass="center" />
						<ItemTemplate>
							<asp:Image id="imgActive" runat="server" />
						</ItemTemplate>
					</asp:TemplateField>
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
