<%@ Page Language="C#" MasterPageFile="~/components/Commerce/ShopMain.Master" AutoEventWireup="true" CodeBehind="ListOrders.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.ListOrders" Title="Untitled Page" %>
<%@ Register Src="~/Common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" runat="server">
<div id="dCompMain" class="Components-Commerce-List-Orders-aspx">
	<div class="fullBox">
	<div class="wrap">
	<h1>Commerce</h1>

    <fieldset>
	    <legend>Filter and search</legend>		
		<div class="formItem">
			<asp:Label cssClass="labelLong" ID="lblShow" runat="server" AssociatedControlID="drpStatuses" SkinId="labelLong" text="Order status:" />
			<asp:DropDownList runat="server" ID="drpStatuses"/>
		</div>
		<div class="formItem clearLeft">
			<asp:Label cssClass="labelLong" ID="lblFromDate" runat="server" AssociatedControlID="dtiFrom" SkinId="labelLong" text="From:"/>
			<uc1:DateTimeCalendar ID="dtiFrom" runat="server" />
		</div>
		<div class="formItem clearLeft">
			<asp:Label cssClass="labelLong" ID="lblToDate" runat="server" AssociatedControlID="dtiTo" SkinId="labelLong" text="To:"/>
			<uc1:DateTimeCalendar ID="dtiTo" runat="server" />
		</div>
		<div class="formCommands clearLeft">
			<asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Show"/>&nbsp;
			<asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Show All" />
			<asp:Button runat="server" id="btnSetStatus" OnClick="btnSetStatus_Click" text="Set Status" visible="false" />
		</div>
    </fieldset>

	<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>

	<asp:GridView
		ID="gvOrders"
		runat="server"
		OnRowCreated="gvOrders_RowCreated"
		DataKeyNames="Id" 
		OnSorting="gvOrders_Sorting"
		OnPageIndexChanging="gvOrders_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvOrders_Editing" 
		OnRowDeleting="gvOrders_Deleting"
		OnRowCommand="gvOrders_RowCommand"
		OnRowDataBound="gvOrders_RowDataBound"
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
			<asp:TemplateField headertext="Customer">
				<ItemTemplate>
					<asp:Label id="lblCustomer" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField headertext="Status">
				<ItemTemplate>
					<asp:Label id="lblStatus" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField headerText="Sum" DataField="Sum" SortExpression="Sum" DataFormatString="{0:F2}"/>
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
