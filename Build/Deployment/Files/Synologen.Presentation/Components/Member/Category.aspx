<%@ Page Language="C#" MasterPageFile="~/components/Member/MemberMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Category" Title="Untitled Page" Codebehind="Category.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phMember" Runat="Server">
 <div id="dCompMain" class="Components-Member-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h1>Member</h1>
	        <fieldset>
		        <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		        <div class="formItem">
		            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
					<asp:RequiredFieldValidator id="rfvFirstName" runat="server" errormessage="Name missing" controltovalidate="txtName" Display="Dynamic">(Name missing!)</asp:RequiredFieldValidator>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>
		        <div id="dGroups" class="formItem clearLeft" runat="server" visible="false">
		            <asp:Label ID="lblGroup" runat="server" AssociatedControlID="drpGroups" SkinId="Long"/>
		            <asp:DropDownList runat="server" ID="drpGroups"/>
		        </div>
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btnSmall"/>
		        </div>
	        </fieldset>
        

            <asp:GridView ID="gvCategory" 
                runat="server" 
                OnRowCreated="gvCategory_RowCreated" 
                DataKeyNames="cCategoryId" 
                SkinID="Striped" 
                OnRowEditing="gvCategory_Editing" 
                OnRowDeleting="gvCategory_Deleting" 
                OnRowCommand="gvCategory_RowCommand">
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

