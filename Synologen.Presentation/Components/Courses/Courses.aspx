<%@ Page Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.Courses" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCourses" Runat="Server">
<div id="dCompMain" class="Components-Courses-Index-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Courses</h1>

    <fieldset>
	    <legend>Filter and search</legend>		
            <div ID="divDrpMain" runat="server" class="formItem clearLeft">
                <label for="drpMain" class="labelLong">Main Course</label>
                <asp:DropDownList runat="server" ID="drpMain" DataTextField=cName DataValueField=cId/>
            </div>            	    
            <div class="formItem">
                <label for="drpCities" class="labelLong">City</label>
                <asp:DropDownList runat="server" ID="drpCities" DataTextField=cCity DataValueField=cId/>
            </div>
                        
	        <div class="formItem clearLeft">
	            <label for="txtSearch">Search criteria</label>
	            <asp:TextBox runat="server" ID="txtSearch"/>
	            <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="Search"/>
	        </div>
    </fieldset>

	<div class="wpcPager">
        <WPC:PAGER id="pager" runat="server" />
    </div>

	<asp:GridView
		ID="gvCourses"
		runat="server"
		DataKeyNames="cId" 
		OnSorting="gvCourses_Sorting"
		OnPageIndexChanging="gvCourses_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvCourses_Editing" 
		OnRowDeleting="gvCourses_Deleting"
		OnSelectedIndexChanged="gvCourses_Applications"
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
			<asp:BoundField headerText="Heading" DataField="cHeading" SortExpression="cHeading"/>
			<asp:BoundField headerText="Contact" DataField="cContactName" SortExpression="cContactName" />
			<asp:BoundField headerText="City" DataField="cCityName" SortExpression="cCityName" />
			<asp:ButtonField headerText="Applications" DataTextField="cApplications" DataTextFormatString="&lt;img src='../../common/icons/attach.png' alt='View applications' border='0'/&gt;&nbsp;({0})" CommandName="Select"/>
			<asp:BoundField headerText="Attending" DataField="cNoOfParticipants" SortExpression="cNoOfParticipants" />
			<asp:BoundField headerText="Last Application Date" DataField="cLastApplicationDate" dataformatstring="{0:yyyy-MM-dd}" HtmlEncode="false" SortExpression="cLastApplicationDate"/>
            <asp:TemplateField HeaderText="Course Period" SortExpression="cCourseStartDate">
                <ItemTemplate>
                    <div>
                        <%# String.Format("{0:yyyy-MM-dd}",DataBinder.Eval(Container.DataItem, "cCourseStartDate"))%>
                         - 
                        <%# String.Format("{0:yyyy-MM-dd}",DataBinder.Eval(Container.DataItem, "cCourseEndDate"))%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
			<asp:ButtonField HeaderText="Edit" Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
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

