<%@ Page Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.Index" Title="Untitled Page" Codebehind="Index.aspx.cs" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCourses" Runat="Server">
<div id="dCompMain" class="Components-Courses-Index-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Main Courses</h1>

    <fieldset>
	    <legend>Filter and search</legend>		
	    <div class="clearLeft">
	        <div class="formItem clearLeft">
	            <label for="drpCategories" class="labelLong">Category</label>
	            <asp:DropDownList runat="server" DataValueField="cCategoryId" DataTextField="cName" ID="drpCategories"/>
	        </div>
	        <div class="formItem clearLeft">
	            <label for="txtSearch">Search criteria</label>
	            <asp:TextBox runat="server" ID="txtSearch"/>
	            <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="Search"/>
	        </div>
	    </div>
    </fieldset>

	<div class="wpcPager">
        <WPC:PAGER id="pager" runat="server" />
    </div>

	<asp:GridView
		ID="gvCourseMains"
		runat="server"
		DataKeyNames="cId" 
		OnSorting="gvCourseMains_Sorting"
		OnPageIndexChanging="gvCourseMains_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvCourseMains_Editing" 
		OnRowDeleting="gvCourseMains_Deleting"
		OnSelectedIndexChanged="gvCourseMains_Courses"
		AllowSorting="true">
		<Columns>
			 <asp:TemplateField ItemStyle-CssClass="center">
					<HeaderTemplate>
						<asp:CheckBox ID="chkSelectHeader" runat="server" OnCheckedChanged="chkSelectHeader_CheckedChanged" AutoPostBack="true" />
					</HeaderTemplate>
					<ItemTemplate>
						<asp:CheckBox ID="chkSelect" runat="server" />
					</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField headerText="Id" DataField="cId" SortExpression="cId"/>
			<asp:BoundField headerText="Name" DataField="cName" SortExpression="cName"/>
			<asp:BoundField headerText="Description" DataField="cDescription" SortExpression="cDescription" />
		    <asp:ButtonField headerText="Courses" DataTextField="cCourses" DataTextFormatString="&lt;img src='../../common/icons/attach.png' alt='View courses' border='0'/&gt;&nbsp;({0})" CommandName="Select"/>
			<asp:ButtonField headerText="Edit" Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
			<asp:TemplateField HeaderText="Delete"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Delete" CssClass="btnSmall" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
</div>
</div>
</div>
</asp:Content>
