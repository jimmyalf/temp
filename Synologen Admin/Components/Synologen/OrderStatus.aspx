<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="OrderStatus.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.OrderStatus" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-OrderStatus-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Orderstatus</h2>
	        <fieldset>
		        <legend><asp:Literal id="ltHeading" runat="server" Text="Lägg till artikel"/></legend>		
		        <div class="formItem">
		            <label class="labelLong">Statusnamn</label>
					<asp:RequiredFieldValidator id="rfvStatusName" runat="server" errormessage="Namn saknas" controltovalidate="txtName" Display="Dynamic" ValidationGroup="Error">(*)</asp:RequiredFieldValidator>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>		
		        <div class="formItem">
		            <label class="labelLong">Ordningstal</label>
		            <asp:CompareValidator id="OrderValidation" runat="server" ErrorMessage="Fel format (skall vara heltal)" ControlToValidate="txtOrderNumber" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" ValidationGroup="Error">(*)</asp:CompareValidator>
					<asp:RequiredFieldValidator id="rfvOrderNumber"  runat="server" errormessage="Ordningstal saknas" controltovalidate="txtOrderNumber" Display="Dynamic" ValidationGroup="Error">(*)</asp:RequiredFieldValidator>		            
		            <asp:TextBox runat="server" ID="txtOrderNumber"/>		            
		        </div>			                	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara" CssClass="btnSmall" ValidationGroup="Error"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvStatus" 
				OnRowDeleting="gvStatus_Deleting"
				OnRowEditing="gvStatus_Editing" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField HeaderText="Namn" DataField="cName" SortExpression="cName"/>
                    <asp:BoundField HeaderText="Ordningstal" DataField="cOrder" SortExpression="cOrder"/>
                    <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn"/>
		            <asp:TemplateField Headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn">
			            <ItemTemplate>
                            <asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" Enabled="false" />
			            </ItemTemplate>
		            </asp:TemplateField>
                </Columns>
            </asp:GridView>          
        </div>
      </div>
    </div>
</asp:Content>
