<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Attribute.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.Attribute" %>

<div class="formItem clearLeft">
        <asp:Label ID="lblName" runat="server" AssociatedControlID="txtValue" SkinId="Long"/>
        <asp:TextBox ID="txtValue" runat="server" TextMode="SingleLine" SkinID="Wide"></asp:TextBox>
		<asp:button ID="btnUp" runat="server" Visible="true" CommandName="Up" OnClick="btnUp_Click" Text="Up" SkinId="Small"/>
		<asp:button ID="btnDown" runat="server" Visible="true" CommandName="Down" OnClick="btnDown_Click" Text="Down" SkinId="Small"/>
		<asp:button ID="btnChange" runat="server" Visible="false" CommandName="Change" OnClick="btnChange_Click" Text="Change" SkinId="Small"/>
		<asp:button ID="btnRemove" runat="server" Visible="false" CommandName="Remove" OnClick="btnRemove_Click" Text="Remove" SkinId="Small"/>
</div>