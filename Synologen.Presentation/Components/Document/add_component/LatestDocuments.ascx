<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestDocuments.ascx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.add_component.LatestDocuments" %>
<div class="Component-Document-AddComponent-LatestDocuments-ascx fullBox">
<div class="wrap">
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
		<div class="formItem">
		    <asp:Label ID="lblNodes" runat="server" AssociatedControlID="drpNodes" SkinId="Long"/>
		    <asp:DropDownList ID="drpNodes" runat="server"/>
		</div>
		<div class="formItem">
		    <asp:Label ID="lblMax" runat="server" AssociatedControlID="txtMax" SkinId="Long"/>
            <asp:TextBox ID="txtMax" runat="server"></asp:TextBox></div>
	</fieldset>
</div>
</div>