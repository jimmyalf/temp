<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="ShopCategories.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.ShopCategories" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Butikkategorier</h2>
	        <fieldset>
		        <legend><asp:Literal id="ltHeading" runat="server" Text="Lägg till artikel"/></asp:Literal></legend>		
		        <div class="formItem">
		            <label class="labelLong">Artikelnamn</label>
					<asp:RequiredFieldValidator id="rfvArticleName" runat="server" errormessage="Namn saknas" controltovalidate="txtName" Display="Dynamic" ValidationGroup="Save">(Namn saknas!)</asp:RequiredFieldValidator>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>		        	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara" CssClass="btnSmall" ValidationGroup="Save"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvCategories" 
				OnRowDeleting="gvCategories_Deleting"
				OnRowEditing="gvCategories_Editing" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Namn" DataField="cName" SortExpression="cName" />
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
