<%@ Page Language="C#" MasterPageFile="~/components/Commerce/CommerceMain.Master" AutoEventWireup="true" CodeBehind="EditMail.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.EditMail" Title="Untitled Page" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" runat="server">
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-Commerce-EditMails-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>
	
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
								
		<div class="formItem clearLeft">
                <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
                <asp:TextBox ID="txtName" runat="server" TextMode="SingleLine" SkinID="Wide"/>
                <asp:RequiredFieldValidator ID="vldReqName" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Name is required.">Name is required.</asp:RequiredFieldValidator>
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblActive" runat="server" AssociatedControlID="chkActive" SkinId="Long"/>
                <asp:CheckBox ID="chkActive" runat="server" TextMode="SingleLine" SkinID="Wide"/>
		</div>

		<div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
		</div>
	</fieldset>
		
	<fieldset>
		<legend><asp:Label ID="lblHeaderAddresses" runat="server"></asp:Label></legend>

	<!--	<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>-->

		<asp:GridView
			ID="gvAddresses"
			runat="server"
			OnRowCreated="gvAddresses_RowCreated"
			DataKeyNames="Id" 
			OnSorting="gvAddresses_Sorting"
			OnPageIndexChanging="gvAddresses_PageIndexChanging"
			SkinID="Striped"
			OnRowEditing="gvAddresses_Editing" 
			OnRowDeleting="gvAddresses_Deleting"
			OnRowCommand="gvAddresses_RowCommand"
			OnRowDataBound="gvAddresses_RowDataBound"
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
				<asp:BoundField headerText="Email" DataField="Email" SortExpression="Email"/>
				<asp:TemplateField headertext="Name">
					<ItemTemplate>
						<asp:CheckBox ID="chkActive" runat="server" />
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
		<asp:button ID="btnAddEmailAddress" runat="server" CommandName="Add Email Address" OnClick="btnAddEmailAddress_Click" Text="Add Email Address" SkinId="Big"/>
	</fieldset>																							
</div>
</div>
</div>
</asp:Content>
