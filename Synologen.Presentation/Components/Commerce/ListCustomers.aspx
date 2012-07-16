<%@ Page Language="C#" MasterPageFile="~/components/Commerce/ShopMain.Master" AutoEventWireup="true"
	Codebehind="ListCustomers.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.ListCustomers"
	Title="Untitled Page" %>

<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" runat="server">
	<div id="dCompMain" class="Components-Commerce-Index-aspx">
		<div class="fullBox">
			<div class="wrap">
				<h1>
					Commerce</h1>
				<fieldset>
					<legend>Filter and search</legend>
					<div class="formItem clearLeft">
						<asp:label id="lblSearch" runat="server" associatedcontrolid="txtSearch" skinid="labelLong"
							text="Search criteria" />
						<asp:textbox runat="server" id="txtSearch" />
						<asp:button runat="server" id="btnSearch" onclick="btnSearch_Click" text="Search" />
					</div>
				</fieldset>
				<div class="wpcPager">
					<WPC:Pager ID="pager" runat="server" /></div>
				<asp:gridview id="gvCustomers" runat="server" datakeynames="CustomerId,CustomerType" onsorting="gvCustomers_Sorting"
					onpageindexchanging="gvCustomers_PageIndexChanging" skinid="Striped" onrowediting="gvCustomers_Editing"
					onrowdeleting="gvCustomers_Deleting" onrowcommand="gvCustomers_RowCommand" onrowdatabound="gvCustomers_RowDataBound"
					allowsorting="true">
		<Columns>
			 <asp:TemplateField ItemStyle-CssClass="center">
					<HeaderTemplate>
						<asp:CheckBox ID="chkSelectHeader" runat="server" OnCheckedChanged="chkSelectHeader_CheckedChanged" AutoPostBack="true" />
					</HeaderTemplate>
					<ItemTemplate>
						<asp:CheckBox ID="chkSelect" runat="server" />
					</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField visible="false">
				<ItemTemplate>
					<asp:Label id="lblCustomerType" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField headertext="Name">
				<ItemTemplate>
					<asp:Label id="lblName" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField headertext="Address">
				<ItemTemplate>
					<asp:Label id="lblAddress" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField headertext="Email">
				<ItemTemplate>
					<asp:Label id="lblEmail" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField headertext="Orders">
				<ItemTemplate>
					<asp:Label id="lblOrders" runat="server"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:ButtonField Text="See Orders" CommandName="Orders" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
			<asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
			<asp:TemplateField>
				<ItemTemplate>
					<asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Delete" CssClass="btnSmall" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:gridview>
			</div>
		</div>
	</div>
</asp:Content>
