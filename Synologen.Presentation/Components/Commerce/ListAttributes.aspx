<%@ Page Language="C#" MasterPageFile="~/components/Commerce/ProductsMain.Master" AutoEventWireup="true" CodeBehind="ListAttributes.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.ListAttributes" %>
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
		ID="gvAttributes"
		runat="server"
		OnRowCreated="gvAttributes_RowCreated"
		DataKeyNames="Id" 
		OnSorting="gvAttributes_Sorting"
		OnPageIndexChanging="gvAttributes_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvAttributes_Editing" 
		OnRowDeleting="gvAttributes_Deleting"
		OnRowCommand="gvAttributes_RowCommand"
		OnRowDataBound="gvAttributes_RowDataBound"
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
			<asp:BoundField headerText="Name" DataField="Name" SortExpression="Name"/>			
			<asp:BoundField headerText="Default Value" DataField="DefaultValue" SortExpression="DefaultValue"/>
			<asp:TemplateField ItemStyle-CssClass="center" HeaderText="Move">
				<ItemTemplate>
					<asp:Button Text="Up" runat="server" CommandName="Up"  ButtonType="Button" ControlStyle-CssClass="btnSmall" CommandArgument='<%# Eval("Id") %>' />
					<asp:Button Text="Down" runat="server" CommandName="Down" ButtonType="Button" ControlStyle-CssClass="btnSmall" CommandArgument='<%# Eval("Id") %>' />
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
