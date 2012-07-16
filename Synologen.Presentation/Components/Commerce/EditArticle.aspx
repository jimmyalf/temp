<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/components/Commerce/ProductsMain.master" AutoEventWireup="true" CodeBehind="EditArticle.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.EditArticle" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<%@ Register Src="ProductImage.ascx" TagName="ProductImage" TagPrefix="uc3" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="Attributes.ascx" TagName="Attributes" TagPrefix="uc3" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc2" %>
<%@ Register Src="~/CommonResources/CommonControls/FileManagerControl/FileManagerControl.ascx" TagName="FileManagerControl" TagPrefix="uc4" %>
<%@ Register Src="ListProductFiles.ascx" TagName="ListProductFiles" TagPrefix="uc5" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" Runat="Server">
<asp:ScriptManager id="scriptManager" runat="server" />	
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-Commerce-EditArticle-aspx">
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
			<uc2:WpcWysiwyg ID="txtDescription" Mode="WpcInternalAdvanced" runat="server" />
		</div>

		<div class="formItem clearLeft">
			<div class="formItem clearLeft">
					<asp:Label ID="lblProdNo" runat="server" AssociatedControlID="txtProdNo" SkinId="Long"/>
					<asp:TextBox ID="txtProdNo" runat="server" TextMode="SingleLine" SkinID="Wide"/>
			</div>
			
			<asp:Placeholder runat="server" visible="false">
				<div class="formItem clearLeft">
						<asp:Label ID="lblDisplayType" runat="server" AssociatedControlID="drpDisplayType" SkinId="Long"/>
						<asp:DropDownList ID="drpDisplayType" runat="server" SkinID="Wide"/>
				</div>
			</asp:Placeholder>
			
			<div class="formItem clearLeft">
					<asp:Label ID="lblPrice" runat="server" AssociatedControlID="txtPrice" SkinId="Long"/>
					<asp:TextBox ID="txtPrice" runat="server" TextMode="SingleLine" SkinID="Wide"></asp:TextBox>
					<asp:RequiredFieldValidator ID="vldReqPrice" runat="server" ControlToValidate="txtPrice" Display="Dynamic" ErrorMessage="Price is required." Visible="false">Price is required.</asp:RequiredFieldValidator>
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
			<asp:PlaceHolder ID ="phProductFiles" visible="false" runat="server">
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
					<asp:checkbox ID="chkShowFileManager" OnCheckedChanged="chkShowFileManager_CheckedChanged" AutoPostBack="true" runat="server" Text="Attach file" SkinId="Long" CausesValidation="False"/>
					<uc4:FileManagerControl ID="ucFileManager" Visible="false" AllowUpload="True" HeadingLevel="3" runat="server" ShowInternalNavigation="True"	UniqueClientID="Test-File-Manager" SelectFileText="Attach selected file"/>
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
			<asp:button ID="btnCreate" runat="server" CommandName="Create" OnClick="btnCreate_Click" Text="Create" SkinId="Big"/>
		    <asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
		    <asp:button ID="btnBack" runat="server" CommandName="Back" OnClick="btnBack_Click" Text="Back" CausesValidation="false" SkinId="Big"/>
		</div>
	</fieldset>
																							
</div>
</div>
</div>
</asp:Content>
