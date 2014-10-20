<%@ Page Language="C#" MasterPageFile="~/components/Campaign/CampaignMain.master" AutoEventWireup="true" CodeFile="FileCategories.aspx.cs" Inherits="components_Campaign_FileCategories" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCampaign" Runat="Server">
<div id="dCompMain" class="Components-Campaign-FileCategories-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h1>Campaign</h1>
	        <fieldset>
		        <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		        <div class="formItem">
		            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
		            <asp:TextBox runat="server" ID="txtName"/><br />
		            <asp:CheckBox runat="server" ID="chkMustOrder" Text="Must order" />
		        </div>
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server"  Text="Save" CssClass="btnSmall" OnClick="btnSave_Click" />
		        </div>
	        </fieldset>
	        <asp:GridView ID="gvFileCategory" 
                runat="server" 
                OnRowCreated="gvFileCategory_RowCreated" 
                DataKeyNames="cId" 
                SkinID="Striped" 
                OnRowEditing="gvFileCategory_Editing" 
                OnRowDeleting="gvFileCategory_Deleting" 
                OnRowCommand="gvFileCategory_RowCommand"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField headerText="Name" DataField="cDescription" SortExpression="cDescription"/>
                    <asp:BoundField headerText="MustOrder" DataField="cMustOrder" SortExpression="cMustOrder"/>
                    <asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
		            <asp:TemplateField headertext="Delete">
			            <ItemTemplate>
                            <asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" commandname="Delete" text="Delete" CssClass="btnSmall" />
			            </ItemTemplate>
		            </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
      </div>
    </div>
</asp:Content>

