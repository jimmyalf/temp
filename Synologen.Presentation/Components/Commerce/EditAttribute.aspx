<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/components/Commerce/ProductsMain.master" AutoEventWireup="true" CodeBehind="EditAttribute.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.EditAttribute" %>
<%@ Register Src="ProductImage.ascx" TagName="ProductImage" TagPrefix="uc3" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" Runat="Server">
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-Commerce-EditAttribute-aspx">
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
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" SkinId="Long"/>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="Wide"></asp:TextBox>
		</div>
																									
		<div class="formItem clearLeft">
                <asp:Label ID="lblDefaultValue" runat="server" AssociatedControlID="txtDefaultValue" SkinId="Long"/>
                <asp:TextBox ID="txtDefaultValue" runat="server" TextMode="MultiLine" SkinID="Wide"></asp:TextBox>
		</div>

		<div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
		</div>
	</fieldset>
</div>
</div>
</div>
</asp:Content>
