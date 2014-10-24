<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="ContractCompanies.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.ContractCompanies" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractCompanies-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h1>Avtalsföretag</h1>
            
       		<asp:PlaceHolder id="plFilterByContract" runat="server">        	                	                  	                 	                
			<fieldset><legend>Filter</legend>
				<div class="formItem clearLeft">
					<label >Visar Företag för Avtalskund:</label>&nbsp;<span><%=SelectedContract.Name %></span><br /><br />
					<input type="button" name="inputNew" class="btnBig" onclick="window.location='EditContractCompany.aspx?contractId=<%=SelectedContract.Id %>'" value="Skapa nytt avtalsföretag" />
					<input type="button" name="inputBack" class="btnBig" onclick="window.location='Contracts.aspx'" value="Tillbaka" />
				</div> 	   	                                                     
			</fieldset>
            </asp:PlaceHolder>	
            
            <br /><br />
            <%--<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>--%>
            <asp:GridView ID="gvContractCompanies" 
				OnRowDeleting="gvContractCompanies_Deleting"
				OnRowEditing="gvContractCompanies_Editing" 
                runat="server" 
                DataKeyNames="cId" 
                OnSorting="gvContractCompanies_Sorting" 
                OnPageIndexChanging="gvContractCompanies_PageIndexChanging" 
				SkinID="Striped" 
                AllowSorting="true">
                <Columns>
					<asp:BoundField headerText="SPCS Id" DataField="cCompanyCode" />
					<asp:BoundField headerText="Org Nr" DataField="cOrganizationNumber" />
                    <asp:BoundField headerText="Företag" DataField="cName" />
                    <asp:BoundField headerText="Valideringar" DataField="cNumberOfValidationRules" />
                    <asp:TemplateField HeaderText="Faktureringstyp">
                        <ItemTemplate>
                            <asp:Literal runat="server" Text='<%#GetInvoiceMethodText((int)Eval("cInvoicingMethodId")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField headerText="Ort" DataField="cCity"/>
                    
                    
					<asp:TemplateField headertext="Aktiv">
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
