<%@ Page Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="Cities.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.Cities" %>
<asp:Content ID="c1" runat="server" ContentPlaceHolderID="phCourses">
    <div id="dCompMain" class="Components-Courses-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
	            <h1>Cities</h1>
	            <fieldset>
	                <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		            <div class="clearLeft">
		                <div class="formItem clearLeft">
	                        <label for="txtName"  class="labelLong">City Name</label>
	                        <asp:TextBox runat="server" ID="txtName"/>
	                        <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"/>
		                </div>
		                <div class="formItem">
		                    <br />
		                    <label id="lblDeleteError" runat="server"  style="Color:Red"/>
		                </div>	                
		            </div>
	            </fieldset>
	            <br /><br />	            
                <asp:GridView 
                    ID="gvCities" 
                    runat="server" 
                    OnRowCreated="gvCities_RowCreated" 
                    DataKeyNames="cId" 
                    SkinID="Striped" 
                    OnRowEditing="gvCities_Editing" 
                    OnRowDeleting="gvCities_Deleting" 
                    OnRowCommand="gvCities_RowCommand"  
                    AllowSorting="true">
                    <Columns>
                        <asp:BoundField headerText="City" DataField="cCity" SortExpression="cCity"/>
                        <asp:ButtonField headerText="Edit" Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
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

