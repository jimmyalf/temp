<%@ Page Language="C#" MasterPageFile="~/components/QuickMail/QuickMailMain.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Spinit.Wpc.QuickMail.Presentation.Index" Title="Untitled Page" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntQuickMailMain" ContentPlaceHolderID="ComponentQuickMail" runat="server">
<div id="dCompMain" class="Components-QuickMail-Index-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>QuickMail</h1>

    <fieldset>
	    <legend>Filter and search</legend>		
	    <div class="formItem">
	        <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpTypes" SkinId="labelLong"/>
	        <asp:DropDownList runat="server" ID="drpTypes"/>&nbsp;
	        <asp:Label ID="lblShowComponents" runat="server" AssociatedControlID="drpComponents" SkinId="labelLong"/>
	        <asp:DropDownList runat="server" ID="drpComponents"/>&nbsp;
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
		ID="gvMails"
		runat="server"
		OnRowCreated="gvMails_RowCreated"
		DataKeyNames="Id" 
		OnSorting="gvMails_Sorting"
		OnPageIndexChanging="gvMails_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvMails_Editing" 
		OnRowDeleting="gvMails_Deleting"
		OnRowCommand="gvMails_RowCommand"
		OnRowDataBound="gvMails_RowDataBound"
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
			<asp:BoundField headerText="Id" DataField="Id" />
			<asp:TemplateField headertext="MailType">
				<ItemTemplate>
					<asp:Label id="lblStatus" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField headertext="Sent">
				<ItemTemplate>
					<asp:Label id="lblSent" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField headerText="Name" DataField="Name" />
			<asp:TemplateField headertext="Component">
				<ItemTemplate>
					<asp:Label id="lblComponent" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
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
