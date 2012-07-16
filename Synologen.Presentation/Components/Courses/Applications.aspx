<%@ Page Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="Applications.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.Applications" %>
<asp:Content ID="c1" runat="server" ContentPlaceHolderID="phCourses">
    <div id="dCompMain" class="Components-Courses-Applications-aspx">
        <div class="fullBox">
            <div class="wrap">
	            <h1>Applications</h1>
	            <fieldset>
	                <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		            <div class="clearLeft">
		                <div ID="divDrpCourses" runat="server" class="formItem clearLeft">
                            <label for="drpCourses" class="labelLong">Courses</label>
                            <asp:DropDownList runat="server" ID="drpCourses" DataTextField="cHeading" DataValueField="cId"/>
                        </div>          
		                <div class="formItem clearLeft">
	                        <label for="txtName"  class="labelLong">Search filter</label>
	                        <asp:TextBox runat="server" ID="txtName"/>
	                        <asp:button ID="btnSave" runat="server" OnClick="btnSearch_Click" Text="Search"/>
		                </div>
		            </div>
	            </fieldset>
	            <br /><br />
                <asp:GridView 
                    ID="gvApplications" 
                    runat="server" 
                    OnRowCreated="gvApplications_RowCreated" 
                    DataKeyNames="cId" 
                    SkinID="Striped" 
                    OnRowEditing="gvApplications_Editing" 
                    OnRowDeleting="gvApplications_Deleting" 
                    OnRowCommand="gvApplications_RowCommand" 
                    OnSorting="gvApplications_Sorting" 
                    AllowSorting="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Name" SortExpression="cLastName">
                            <ItemTemplate>
                                <div>
                                    <%# DataBinder.Eval(Container.DataItem, "cFirstName")%>
                                     
                                    <%# DataBinder.Eval(Container.DataItem, "cLastName")%>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Course" DataField="cHeading" SortExpression="cHeading" />
                        <asp:BoundField HeaderText="Cource Location" DataField="cCity" SortExpression="cCity" />                        
                        <asp:BoundField HeaderText="Company" DataField="cCompany" SortExpression="cCompany" />
                        <asp:BoundField HeaderText="Participants" DataField="cNrOfParticipants" SortExpression="cNrOfParticipants" />
                        <asp:BoundField HeaderText="Application Date" DataField="cCreatedDate" SortExpression="cCreatedDate"  Dataformatstring="{0:yyyy-MM-dd}" HtmlEncode="false"  />
                        <asp:ButtonField Text="View" HeaderText="View" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-CssClass="center"/>
		                <asp:TemplateField headertext="Delete" ItemStyle-CssClass="center">
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


