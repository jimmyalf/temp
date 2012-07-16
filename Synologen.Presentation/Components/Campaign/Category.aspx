<%@ Page Language="C#" MasterPageFile="~/components/Campaign/CampaignMain.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="components_Campaign_Category" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCampaign" Runat="Server">
    <div id="dCompMain" class="Components-Campaign-Category-aspx">
    <div class="fullBox">
    <div class="wrap">
        <h1>Campaign</h1>
	    <fieldset>
		    <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		    <div class="formItem">
		        <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="labelLong"/>
		        <asp:TextBox runat="server" ID="txtName"/>
		    </div>
		    <div class="formCommands">
		        <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" SkinId="btnBig"/>
		    </div>
	    </fieldset>
    

        <asp:GridView ID="gvCategory" 
                        runat="server" 
                        OnRowCreated="gvCategory_RowCreated" 
                        DataKeyNames="cCategoryId" 
                        SkinID="Striped" 
                        OnRowEditing="gvCategory_Editing" 
                        OnRowDeleting="gvCategory_Deleting" 
                        OnRowCommand="gvCategory_RowCommand"
                        AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField headerText="Name" DataField="cName" SortExpression="cName"/>
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

