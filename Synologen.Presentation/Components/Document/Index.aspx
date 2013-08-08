<%@ Page Language="C#" MasterPageFile="~/components/Document/DocumentMain.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.Index" Title="Untitled Page" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phDocument" Runat="Server">
<div id="dCompMain" class="Components-Document-Index-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Document folders</h1>
	

	<asp:GridView
		ID="gvRootNodes"
		runat="server"
		OnRowCreated="gvRootNodes_RowCreated"
		DataKeyNames="Id" 
		OnSorting="gvRootNodes_Sorting"
		OnPageIndexChanging="gvRootNodes_PageIndexChanging"
		SkinID="Striped"
		OnRowEditing="gvRootNodes_Editing" 
		OnRowDeleting="gvRootNodes_Deleting"
		OnRowCommand="gvRootNodes_RowCommand"
		OnRowDataBound="gvRootNodes_RowDataBound"
		AllowSorting="false">
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
			<asp:BoundField headerText="Name" DataField="Name" />
			<asp:ButtonField Text="Open" CommandName="Documents"  ButtonType="Button" ControlStyle-CssClass="btnSmall" />
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
	
