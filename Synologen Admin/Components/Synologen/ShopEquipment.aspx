<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="ShopEquipment.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.ShopEquipment" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">

<div id="dCompMain" class="Components-Synologen-ShopEquipment-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Butiksutrustning</h2>
	        <fieldset>
		        <legend><asp:Literal id="ltHeading" runat="server" Text="Lägg till artikel"/></legend>		
		        <div class="formItem">
		            <label class="labelLong">Namn</label>
					<asp:RequiredFieldValidator id="rfvName" runat="server" errormessage="Namn saknas" controltovalidate="txtName" Display="Dynamic" ValidationGroup="vldSave"  >(Namn saknas!)</asp:RequiredFieldValidator>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>		 
		        <div class="formItem clearLeft">
		            <label class="labelLong">Beskrivning</label>
		            <asp:TextBox runat="server" ID="txtDescription" cssClass="txtAreaWide" TextMode="MultiLine"/>
		        </div>			               	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara"  SkinId="Big" ValidationGroup="vldSave"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvEquipment" 
				OnRowDeleting="gvEquipment_Deleting"
				OnRowEditing="gvEquipment_Editing" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Namn" DataField="cName" SortExpression="cName"/>
                    <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn" />
		            <asp:TemplateField headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn">
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
