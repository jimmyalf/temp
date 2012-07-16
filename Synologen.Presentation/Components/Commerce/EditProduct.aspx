<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/components/Commerce/ProductsMain.master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.components.Commerce.EditProduct" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<%@ Register Src="ProductImage.ascx" TagName="ProductImage" TagPrefix="uc3" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Attributes.ascx" TagName="Attributes" TagPrefix="uc3" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc2" %>
<%@ Register Src="~/CommonResources/CommonControls/FileManagerControl/FileManagerControl.ascx" TagName="FileManagerControl" TagPrefix="uc4" %>
<%@ Register Src="ListProductFiles.ascx" TagName="ListProductFiles" TagPrefix="uc5" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" Runat="Server">
	<asp:ScriptManager id="scriptManager" runat="server" />	
	<script type="text/javascript">
		//<!--
		function UpdateAllChildren(nodes, checked)
		{
			var i;
			for (i=0; i<nodes.length; i++)
			{
				if (checked) 		
					nodes[i].Check(); 		
				else		
					nodes[i].UnCheck(); 
                
				if (nodes[i].Nodes.length > 0)		
					UpdateAllChildren(nodes[i].Nodes, checked);		
			}
		}

		function AfterCheck(node)
		{
			if (!node.Checked && node.Parent != null)
				{
					node.Parent.UnCheck();	
				}
                
			var siblingCollection = (node.Parent != null) ? node.Parent.Nodes : node.TreeView.Nodes;
            
			var allChecked = true;
			for (var i=0; i<siblingCollection.length; i++)
				{
					if (!siblingCollection[i].Checked)
					{
						allChecked = false;
						break;
					}
				}
			if (allChecked && node.Parent != null)
				{
					node.Parent.Check();
				}
	            
			UpdateAllChildren(node.Nodes, node.Checked);
		}
		//-->
</script>
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-Commerce-EditProduct-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>
	<fieldset>
		<legend><asp:Label ID="lblHeaderCategories" runat="server"></asp:Label></legend>
		<div class="formItem">
            <asp:Label ID="lblCategories" runat="server" AssociatedControlID="radTreeViewCategories" SkinId="Long"></asp:Label>                            
			<div class="clearLeft">
					<telerik:RadTreeView 
						ID="radTreeViewCategories" 
						runat="server" 
						CheckBoxes="True"
						SingleExpandPath="True" 
						AutoPostBackOnCheck="True"
						OnNodeCheck="radTreeViewCategories_NodeCheck"
						ImagesBaseDir="~/Components/Commerce/Img/Outlook" />
			</div>
		</div>
		<div class="formCommands">					    
		    <asp:button ID="btnNext" runat="server" CommandName="Next" OnClick="btnNext_Click" Text="Next" SkinId="Big" visible="false"/>
		</div>
	</fieldset>
	<asp:PlaceHolder ID="phCategory" runat="server">
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
		
		<div id="dWysiwyg" runat="server" class="dWysiwyg">
            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" SkinId="Long"/>
        	<div class="clearLeft">
				<uc2:WpcWysiwyg ID="txtDescription" Mode="WpcInternalBasic" runat="server" />
			</div>
		</div>
		
		<div class="formItem clearLeft">
			<div class="formItem clearLeft">
					<asp:Label ID="lblProdNo" runat="server" AssociatedControlID="txtProdNo" SkinId="Long"/>
					<asp:TextBox ID="txtProdNo" runat="server" TextMode="SingleLine" SkinID="Wide"/>
			</div>
		
			<asp:placeholder runat="server" visible="false">
				<div class="formItem clearLeft">
						<asp:Label ID="lblDisplayType" runat="server" AssociatedControlID="drpDisplayType" SkinId="Long"/>
						<asp:DropDownList ID="drpDisplayType" runat="server" SkinID="Wide"/>
				</div>
			</asp:placeholder>
		
			<div class="formItem clearLeft">
				<asp:Label ID="lblPrice" runat="server" AssociatedControlID="txtPrice" SkinId="Long"/>
				<asp:TextBox ID="txtPrice" runat="server" TextMode="SingleLine" SkinID="Wide"></asp:TextBox>
			</div>		
		
			<div id="dSpotImage" runat="server">
				<div class="formItem clearLeft">
					<asp:Label id="lblSpotImage" runat="server" SkinId="Long" AssociatedControlID="imgProductImage" />
					<uc3:ProductImage ID="imgProductImage" runat="server" />
				</div>    		
			</div>
			
			<div class="formItem clearLeft">
				<asp:Label ID="lblKeyWords" runat="server" AssociatedControlID="txtKeyWords" SkinId="Long"/>
				<asp:TextBox ID="txtKeyWords" runat="server" TextMode="MultiLine" SkinID="Wide"/>
			</div>	
			
			<div class="formItem clearLeft">
				<asp:UpdatePanel runat="server" ID="updAttributeList">
				<ContentTemplate>
				<uc3:Attributes ID="attControl" Type="Product" runat="server" />
				</ContentTemplate>						
				</asp:UpdatePanel>				
			</div>
		</div>
		
		<div class="formItem">
			<asp:PlaceHolder ID ="phProductFiles" Visible="false" runat="server">
				<asp:UpdatePanel runat="server" ID="updEditFileConnection">
				<ContentTemplate>
				<div class="formItem clearLeft">
					<label for="<%=ucListProductFiles.ClientID%>" class="labelLong">Attached files</label>
					<uc5:ListProductFiles ID="ucListProductFiles" runat="server" />
				</div>	
				</ContentTemplate>						
				</asp:UpdatePanel>	
				<div class="formItem clearLeft attach">
				<fieldset><legend>Attach file</legend>
					<asp:CheckBox ID="chkShowFileManager" OnCheckedChanged="chkShowFileManager_CheckedChanged" AutoPostBack="true" runat="server" Text="Attach file" CausesValidation="False"/>
					<uc4:FileManagerControl ID="ucFileManager" Visible="false" AllowUpload="True" HeadingLevel="3" runat="server" ShowInternalNavigation="True"	UniqueClientID="Test-File-Manager" SelectFileText="Attach selected file" />
					<asp:PlaceHolder id="plNewFileSettings" runat="server" Visible="false">
					<div id="product-file-connection-settings">
						<div class="formItem product-file-connection-category">
							<label for="<%=drpProductFileCategory.ClientID%>" class="labelLong">File category</label>
							<asp:DropDownList ID="drpProductFileCategory" runat="server" />
						</div>
						<div class="formItem product-file-connection-name">
							<label for="<%=txtProductFileConnectionName.ClientID%>" class="labelLong">Name</label>
							<asp:TextBox ID="txtProductFileConnectionName" runat="server"/>
						</div>
						<div class="formItem product-file-connection-description">	
							<label for="<%=txtProductFileConnectionDescription.ClientID%>" class="labelLong">Description</label>
							<asp:TextBox ID="txtProductFileConnectionDescription" runat="server"/>
						</div>
					</div>
					</asp:PlaceHolder>
				</fieldset>	
				</div>
			</asp:PlaceHolder>
		</div>		
		
		<div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
		    <asp:button ID="btnCreate" runat="server" CommandName="Create" OnClick="btnCreate_Click" Text="Create" SkinId="Big" visible="false"/>
		</div>
	</fieldset>
		
	<fieldset>
		<legend><asp:Label ID="lblHeaderChilds" runat="server"></asp:Label></legend>

	<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>

		<asp:GridView
			ID="gvArticles"
			runat="server"
			OnRowCreated="gvArticles_RowCreated"
			DataKeyNames="Id" 
			OnSorting="gvArticles_Sorting"
			OnPageIndexChanging="gvArticles_PageIndexChanging"
			SkinID="Striped"
			OnRowEditing="gvArticles_Editing" 
			OnRowDeleting="gvArticles_Deleting"
			OnRowCommand="gvArticles_RowCommand"
			OnRowDataBound="gvArticles_RowDataBound"
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
				<asp:BoundField headerText="Name" DataField="Name" SortExpression="product.name"/>
				<%--
				<asp:ButtonField Text="Up" CommandName="Up"  ButtonType="Button" ControlStyle-CssClass="btnSmall" SortExpression="product.order" headerText="Order" />
				<asp:ButtonField Text="Down" CommandName="Down" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
				--%>
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
		<asp:button ID="btnAddArticle" runat="server" CommandName="Add Article" OnClick="btnAddArticle_Click" Text="Add Article" SkinId="Big"/>
		<asp:button ID="btnDeleteArticles" runat="server" CommandName="Delete Marked Articles" OnClick="btnDeleteArticles_Click" Text="Delete Marked Articles" SkinId="Big"/>
	</fieldset>	
	</asp:PlaceHolder>																						
</div>
</div>
</div>
</asp:Content>
