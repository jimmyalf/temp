<%@ Page Language="C#" MasterPageFile="~/components/Commerce/ProductsMain.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.components.Commerce.Index" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" Runat="Server">
<div id="dCompMain" class="Components-Commerce-Index-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>

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
		ID="gvProducts"
		runat="server"
		OnRowCreated="gvProducts_RowCreated"
		DataKeyNames="Id" 
		OnSorting="gvProducts_Sorting"
		OnPageIndexChanging="gvProducts_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvProducts_Editing" 
		OnRowDeleting="gvProducts_Deleting"
		OnRowCommand="gvProducts_RowCommand"
		OnRowDataBound="gvProducts_RowDataBound"
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
			<asp:BoundField headerText="Id" DataField="Id" SortExpression="product.id"/>
			<asp:BoundField headerText="Product number" DataField="PrdNo" SortExpression="product.prdno"/>
			<asp:BoundField headerText="Name" DataField="Name" SortExpression="product.name"/>
			<asp:TemplateField ItemStyle-CssClass="center" headerText="Move">
				<ItemTemplate>
					<asp:Button id="btnUp" runat="server" Text="Up" CommandName="Up"  ControlStyle-CssClass="btnSmall" headerText="Order" CommandArgument='<%# Eval("Id") %>'/>
					<asp:Button id="btnDown" runat="server" Text="Down" CommandName="Down" ControlStyle-CssClass="btnSmall" CommandArgument='<%# Eval("Id") %>'/>
				</ItemTemplate>
			</asp:TemplateField>
			<%--
			<asp:ButtonField Text="Up" CommandName="Up"  ButtonType="Button" ControlStyle-CssClass="btnSmall" headerText="Order" SortExpression="product.order"/>
			<asp:ButtonField Text="Down" CommandName="Down" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
			--%>
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
