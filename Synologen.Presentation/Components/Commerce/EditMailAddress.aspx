<%@ Page Language="C#" MasterPageFile="~/components/Commerce/CommerceMain.Master" AutoEventWireup="true" CodeBehind="EditMailAddress.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.EditMailAddress" Title="Untitled Page" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" runat="server">
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-Commerce-EditMailAddress-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>
	
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
								
		<div class="formItem clearLeft">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" SkinId="Long"/>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="SingleLine" SkinID="Wide"/>
                <asp:RequiredFieldValidator ID="vldReqEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Email is required.">Email is required.</asp:RequiredFieldValidator>
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblActive" runat="server" AssociatedControlID="chkActive" SkinId="Long"/>
                <asp:CheckBox ID="chkActive" runat="server" TextMode="SingleLine" SkinID="Wide"/>
		</div>

		<div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
		</div>
	</fieldset>
</div>
</div>
</div>
</asp:Content>
