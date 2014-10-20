<%@ Page Language="C#" MasterPageFile="~/components/Synologen/SynologenMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Category" Title="Medlemskategori" Codebehind="Category.aspx.cs" %>
<%@ Register Src="SynologenSubMenu.ascx" TagName="SubMenu" TagPrefix="syn" %>
<asp:Content runat="server" ContentPlaceHolderID="SubMenuPlaceHolder">
	<syn:SubMenu runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" Runat="Server">
 <div id="dCompMain" class="Components-Synologen-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Medlemskategorier</h2>
	        <fieldset>
		        <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		        <div class="formItem">
		            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
					<asp:RequiredFieldValidator id="rfvFirstName" runat="server" errormessage="Namn saknas" controltovalidate="txtName" Display="Dynamic">(Namn saknas!)</asp:RequiredFieldValidator>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>
		        <div class="formItem">
		            <label class="labelLong">Butik-kategori</label>
		            <asp:DropDownList runat="server" ID="drpShopCategory" DataValueField="cId" DataTextField="cName"/>
		        </div>			        
		        <div id="dGroups" class="formItem clearLeft" runat="server" visible="false">
		            <asp:Label ID="lblGroup" runat="server" AssociatedControlID="drpGroups" SkinId="Long"/>
		            <asp:DropDownList runat="server" ID="drpGroups"/>
		        </div>	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara"  SkinId="Big"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvCategory" 
                runat="server" 
                OnRowCreated="gvCategory_RowCreated" 
                DataKeyNames="cCategoryId" 
                SkinID="Striped" 
                OnRowEditing="gvCategory_Editing" 
                OnRowDeleting="gvCategory_Deleting" 
                OnRowCommand="gvCategory_RowCommand">
                <Columns>
                    <asp:BoundField headerText="Namn" DataField="cName" SortExpression="cName"/>
                    <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center"/>
		            <asp:TemplateField headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"  >
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

