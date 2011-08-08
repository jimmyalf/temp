<%@ Page Language="C#" MasterPageFile="~/components/Member/MemberMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.FileCategories" Title="Untitled Page" Codebehind="FileCategories.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phMember" Runat="Server">
<div id="dCompMain" class="Components-Member-FileCategories-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h1>Member</h1>
	        <fieldset>
		        <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		        <div class="formItem">
		            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server"  Text="Save" OnClick="btnSave_Click" CssClass="btnSmall"/>
		        </div>
	        </fieldset>
	        <asp:GridView ID="gvFileCategory" 
                runat="server" 
                OnRowCreated="gvFileCategory_RowCreated" 
                DataKeyNames="cId" 
                SkinID="Striped" 
                OnRowEditing="gvFileCategory_Editing" 
                OnRowDeleting="gvFileCategory_Deleting" 
                OnRowCommand="gvFileCategory_RowCommand">
                <Columns>
                    <asp:BoundField headerText="Name" DataField="cDescription" SortExpression="cDescription"/>
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

