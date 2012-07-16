<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/components/Commerce/ProductsMain.master" AutoEventWireup="true" CodeBehind="EditProductFileCategory.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.Components.Commerce.EditProductFileCategory" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" Runat="Server">
<div id="dCompMain" class="Components-Commerce-EditProductFileCategory-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>
	<fieldset>
		<legend><%=PageModeText%> Product File Category</legend>
				
		<div class="formItem clearLeft">
            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
            <asp:TextBox ID="txtName" runat="server" TextMode="SingleLine" SkinID="Wide"/>
            <asp:RequiredFieldValidator ID="vldReqName" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Name is required.">Name is required.</asp:RequiredFieldValidator>
		</div>
		
		
		<div class="formCommands">					    
			<asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
			<asp:button ID="btnBack" runat="server" CommandName="Back" OnClick="btnBack_Click" Text="Back" CausesValidation="false" SkinId="Big"/>
		</div>
	</fieldset>																						
</div>
</div>
</div>
</asp:Content>
