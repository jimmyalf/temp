<%@ Page Language="C#" MasterPageFile="~/components/News/NewsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Components.News.Category" Title="Untitled Page" Codebehind="Category.aspx.cs" %>
<asp:Content ID="c1" runat="server" ContentPlaceHolderID="phNews">
    <div id="dCompMain" class="Components-News-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
	            <h1>News</h1>
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
                <asp:GridView ID="gvCategory" runat="server" OnRowCreated="gvCategory_RowCreated" DataKeyNames="cCategoryId" 
                SkinID="Striped" OnRowEditing="gvCategory_Editing" OnRowDeleting="gvCategory_Deleting" OnRowCommand="gvCategory_RowCommand">
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

