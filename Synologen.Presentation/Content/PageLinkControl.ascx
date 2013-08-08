<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.PageLinkControl" Codebehind="PageLinkControl.ascx.cs" %>
<div class="Content-PageLinkControl-ascx fullBox">
<div class="wrap">
<h2>Page link</h2>

<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtTitle" SkinID="Long">Title <ASP:REQUIREDFIELDVALIDATOR id="rfvTitlte" runat="server" controltovalidate="txtTitle" errormessage="Title must be given!">(Title must be given)</ASP:REQUIREDFIELDVALIDATOR></asp:Label>
	<ASP:TEXTBOX id="txtTitle" runat="server"></ASP:TEXTBOX>
</div>

<div class="formItem clearLeft">
	<asp:Label runat="server" AssociatedControlID="txtLink" SkinID="Long">Link</asp:Label>
	<ASP:TEXTBOX id="txtLink" runat="server"></ASP:TEXTBOX>
	<asp:Button ID="btnBrowse" runat="server" Text="Browse for link" ToolTip="Browse for internal link" OnClick="btnBrowse_Click" CausesValidation="False" />
</div>

<div class="formItem clearLeft">
	<asp:Label runat="server" AssociatedControlID="txtTarget" SkinID="Long">Target</asp:Label>
	<ASP:TEXTBOX id="txtTarget" runat="server"></ASP:TEXTBOX>
</div>
</div>
</div>