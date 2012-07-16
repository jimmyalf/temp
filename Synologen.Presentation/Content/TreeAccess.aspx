<%@ Page Language="C#" MasterPageFile="~/content/ContentMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.TreeAccess" Title="WPC Tree Access" Codebehind="TreeAccess.aspx.cs" %>
<asp:Content ID="MoveContent" ContentPlaceHolderID="ComponentPage" Runat="Server">
<div class="Content-TreeAccess-aspx fullBox">
<div class="wrap">
<h1>Edit access</h1>

<p><strong><ASP:LABEL id="lblProperties" runat="server">Properties for</ASP:LABEL></strong></p>

<fieldset>
<legend>Edit access</legend>
<div class="box">
	<asp:Label runat="server" AssociatedControlID="listNonMemberOf" SkinID="Long">Member of has no access</asp:Label>
	<ASP:LISTBOX id="listNonMemberOf" runat="server" datatextfield="cName" datavaluefield="cId" selectionmode="Multiple"></ASP:LISTBOX>
</div>
<div class="box center">
	<input id="btnRemoveRole" type="submit" value="<" title="Move to: has no access" runat="server" onserverclick="btnRemoveRole_ServerClick" class="btnSmall" />
	<input id="btnAddRole" type="submit" value=">" title="Move to: has access" runat="server" onserverclick="btnAddRole_ServerClick" class="btnSmall" />
</div>
<div class="box">
	<asp:Label runat="server" AssociatedControlID="listMemberOf" SkinID="Long">Member of has access</asp:Label>
	<ASP:LISTBOX id="listMemberOf" runat="server" datatextfield="cName" datavaluefield="cId" selectionmode="Multiple"></ASP:LISTBOX>
</div>

<div class="formCommands">
	<ASP:BUTTON id="btnSave" runat="server" text="Save" onclick="btnSave_Click" SkinID="Big" />
</div>
</fieldset>

</div>
</div>
</asp:Content>