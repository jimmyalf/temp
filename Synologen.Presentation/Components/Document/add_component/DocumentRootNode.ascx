<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentRootNode.ascx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.add_component.DocumentRootNode" %>
<div class="Component-Document-AddComponent-DocumentRootNode-ascx fullBox">
<div class="wrap">
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
		<div class="formItem">
		    <asp:Label ID="lblNodes" runat="server" AssociatedControlID="drpNodes" SkinId="Long"/>
		    <asp:DropDownList ID="drpNodes" runat="server"/>
		</div>
	</fieldset>
</div>
</div>