<%@ Page Language="C#" MasterPageFile="~/components/Commerce/ProductsMain.Master" AutoEventWireup="true" CodeBehind="ListProductCategories.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.ListProductCategories" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" Runat="Server">
<div id="dCompMain" class="Components-Commerce-Index-aspx">
	<div class="fullBox">
	<div class="wrap">
	<h1>Commerce</h1>

    <fieldset>
	    <legend>Filter and search</legend>		
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" SkinId="labelLong" Text="Search criteria" />
	        <asp:TextBox runat="server" ID="txtSearch"/>
	        <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="Search"/>
	    </div>
    </fieldset>

	<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>

	<asp:GridView
		ID="gvProductCategories"
		runat="server"
		OnRowCreated="gvProductCategories_RowCreated"
		DataKeyNames="Id" 
		OnSorting="gvProductCategories_Sorting"
		OnPageIndexChanging="gvProductCategories_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvProductCategories_Editing" 
		OnRowDeleting="gvProductCategories_Deleting"
		OnRowCommand="gvProductCategories_RowCommand"
		OnRowDataBound="gvProductCategories_RowDataBound"
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
			<asp:BoundField headerText="Id" DataField="Id" SortExpression="Id"/>
			<asp:BoundField headerText="Heading" DataField="Name" SortExpression="Name"/>
			<asp:TemplateField ItemStyle-CssClass="center"  headerText="Products">
				<ItemTemplate>
					<a href="Index.aspx?category=<%#Eval("Id")%>" title="List connected products">Show category products</a>
				</ItemTemplate>
			</asp:TemplateField>			
			<asp:TemplateField ItemStyle-CssClass="center"  headerText="Move">
				<ItemTemplate>
					<asp:Button id="btnUp" runat="server" Text="Up" CommandName="Up"  ControlStyle-CssClass="btnSmall" headerText="Order" CommandArgument='<%# Eval("Id") %>'/>
					<asp:Button id="btnDown" runat="server" Text="Down" CommandName="Down" ControlStyle-CssClass="btnSmall" CommandArgument='<%# Eval("Id") %>'/>
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
