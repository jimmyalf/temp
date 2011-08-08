<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/components/Commerce/ProductsMain.master" AutoEventWireup="true" CodeBehind="EditProductCategory.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.EditProductCategory" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<%@ Register Src="ProductImage.ascx" TagName="ProductImage" TagPrefix="uc3" %>
<%@ Register Src="Attributes.ascx" TagName="Attributes" TagPrefix="uc3" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc2" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" Runat="Server">
	<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-Commerce-EditProductCategory-aspx">
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
			<asp:checkbox ID="chkShowProductDescription" OnCheckedChanged="chkShowProductDescription_CheckedChanged" AutoPostBack="true" runat="server" />
		</div>
		
		<div id="dWysiwyg" runat="server" class="dWysiwyg clearLeft">
            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" SkinId="Long"/>
			<uc2:WpcWysiwyg ID="txtDescription" Mode="WpcInternalBasic" runat="server" />
		</div>
		
		<div id="dSpotImage" runat="server">
		    <div class="formItem clearLeft">
		        <asp:Label id="lblSpotImage" runat="server" SkinId="Long" AssociatedControlID="imgProductImage" />
		        <uc3:ProductImage ID="imgProductImage" runat="server" />
		    </div>    		
        </div>
        
        <div class="formItem clearLeft" id="dAttributeList">
                <asp:Label ID="lblAttributes" runat="server" AssociatedControlID="chklAttributes" SkinId="Long"></asp:Label>                            
                <asp:CheckBoxList ID="chklAttributes" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="4" RepeatLayout="Table"/>
        </div>
		
		<uc3:Attributes ID="attControl" Type="ProductCategory" runat="server" visible="false" />
		
		<div class="formItem clearLeft" id="dLocations">
                <asp:Label ID="lblLocations" runat="server" AssociatedControlID="chklLocations" SkinId="Long"></asp:Label>                            
                <asp:CheckBoxList ID="chklLocations" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="4" RepeatLayout="Table"/>
		</div>
		
		<div class="formCommands">					    
			<asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
			<asp:button ID="btnBack" runat="server" CommandName="Back" OnClick="btnBack_Click" Text="Back" CausesValidation="false" SkinId="Big"/>
		</div>
	</fieldset>
		
	<fieldset>
		<legend><asp:Label ID="lblHeaderChilds" runat="server"></asp:Label></legend>

	<!--	<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>-->

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
				<asp:TemplateField ItemStyle-CssClass="center" headerText="Move">
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
		<asp:button ID="btnAddSubCategory" runat="server" CommandName="Add Sub Category" OnClick="btnAddSubCategory_Click" Text="Add Sub Category" SkinId="Big"/>
		<asp:button ID="btnDeleteSubCategories" runat="server" CommandName="Delete Marked Sub Categories" OnClick="btnAddSubCategory_Click" Text="Delete Marked Sub Categories" SkinId="Big"/>
	</fieldset>																							
</div>
</div>
</div>
</asp:Content>
