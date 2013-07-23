<%@ Page Language="C#" MasterPageFile="~/components/News/NewsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Components.News.Index" Title="Untitled Page" Codebehind="Index.aspx.cs" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phNews" Runat="Server">
<div id="dCompMain" class="Components-News-Index-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>News</h1>

    <fieldset>
	    <legend>Filter and search</legend>		
	    <div class="formItem">
	        <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpCategories" SkinId="labelLong"/>
	        <asp:DropDownList runat="server" ID="drpCategories"/>&nbsp;
	        <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Show"/>
	        <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Show All" />
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" SkinId="labelLong" Text="Search criteria" />
	        <asp:TextBox runat="server" ID="txtSearch"/>
	        <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="Search"/>
	    </div>
    </fieldset>

	<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>

	<asp:GridView
		ID="gvNews"
		runat="server"
		OnRowCreated="gvNews_RowCreated"
		DataKeyNames="cId" 
		OnSorting="gvNews_Sorting"
		OnPageIndexChanging="gvNews_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvNews_Editing" 
		OnRowDeleting="gvNews_Deleting"
		OnRowCommand="gvNews_RowCommand"
		OnRowDataBound="gvNews_RowDataBound"
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
			<asp:TemplateField headertext="Status">
				<ItemTemplate>
					<asp:Label id="lblStatus" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField headerText="Heading" DataField="cHeading" SortExpression="cHeading"/>
			<asp:BoundField HeaderText="Start Date" DataField="cStartDate" DataFormatString="{0:yyyy-MM-dd}" SortExpression="cStartDate" />
			<asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
			<asp:TemplateField>
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

