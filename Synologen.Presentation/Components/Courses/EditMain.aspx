<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="EditMain.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.EditMain" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="ww" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCourses" Runat="Server">
    <div id="dCompMain" class="Components-Courses-EditMain-aspx">
        <div class="fullBox">
            <div class="wrap">
	            <h1>Edit Main</h1>
                <fieldset><legend>Filter and search</legend>		
                    <div class="formItem clearLeft">
                        <label for="txtName"  class="labelLong">Name *</label>
                        <asp:TextBox runat="server" ID="txtName"/>
                        <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server" ErrorMessage="The Main Course must have a name!" />
                    </div>
                    <div class="formItem clearLeft">
                        <label for="txtDescription"  class="labelLong">Description</label>
                        <asp:TextBox CssClass="inputLong" runat="server" ID="txtDescription"/>
                    </div>    
    	            <div id="dWysiwyg" runat="server" class="dWysiwyg clearLeft">
                        <WW:WpcWysiwyg  ID="txtBody" runat="server" />
                    </div>        
                    <div class="formItem clearLeft">
                        <label for="chkCategories"  class="labelLong">Connected categories *</label>
                        <asp:CheckBoxList runat="server" DataValueField="cCategoryId" DataTextField="cName" ID="chkCategories"/>
                        <div id="divErrorCategories" visible="false" runat="server">
                            <label style="color:Red">At least one category must be selected!</label>
                        </div>
                    </div>                             
                    <div ID="divButtons" class="formCommands">
                        <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="Big" />
                        <asp:Button ID="btnSave" runat="server" OnClick="btn_Save" Text="Save" SkinID="Big" />
                    </div>   
                </fieldset>                     
            </div>
        </div>
    </div>
</asp:Content>
