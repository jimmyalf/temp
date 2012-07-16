<%@ Page Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.Categories" %>
<asp:Content ID="c1" runat="server" ContentPlaceHolderID="phCourses">
    <div id="dCompMain" class="Components-Courses-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
	            <h1>Categories</h1>
	            <fieldset>
	                <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		            <div class="clearLeft">
		                <div class="formItem clearLeft">
	                        <label for="txtName"  class="labelLong">Category Name</label>
	                        <asp:TextBox runat="server" ID="txtName"/>
                            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"/>	                        
		                </div>
		            </div>
	            </fieldset>
	            <br /><br />
                <asp:GridView ID="gvCategory" runat="server" OnRowCreated="gvCategory_RowCreated" DataKeyNames="cCategoryId" 
                    SkinID="Striped" OnRowEditing="gvCategory_Editing" OnRowDeleting="gvCategory_Deleting" OnRowCommand="gvCategory_RowCommand">
                    <Columns>
                        <asp:BoundField headerText="Name" DataField="cName" SortExpression="cName"/>
                        <asp:ButtonField headertext="Edit" Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
		                <asp:TemplateField headertext="Delete" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
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

