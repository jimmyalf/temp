<%@ Page Language="C#" MasterPageFile="~/components/Commerce/CommerceMain.Master" AutoEventWireup="true" CodeBehind="ListMails.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.ListMails" Title="Untitled Page" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" runat="server">
<div id="dCompMain" class="Components-Commerce-ListMails-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>

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
		OnRowCommand="gvMails_RowCommand"
		OnRowDataBound="gvMails_RowDataBound"
		AllowSorting="true">
		<Columns>
			<asp:BoundField headerText="Id" DataField="Id" SortExpression="cId"/>
			<asp:BoundField headerText="Name" DataField="Name" SortExpression="cName"/>
			<asp:TemplateField headertext="Active">
				<ItemTemplate>
					<asp:CheckBox ID="chkActive" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
		</Columns>
	</asp:GridView>

</div>
</div>
</div>
</asp:Content>
