<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternalLink.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.InternalLink"  %>
<%@ Register Src="InternalLinkFirstPage.ascx" TagName="InternalLinkFirstPage" TagPrefix="uc1" %>
<link rel="stylesheet" type="text/css" href="/Common/Css/Dialogs.css" media="screen" />
<script src="<%=Spinit.Wpc.Utility.Business.Globals.ResourceUrl %>CommonControls/Wysiwyg/Scripts/tiny_mce/tiny_mce.js" type="text/javascript"></script>
<script src="<%=Spinit.Wpc.Utility.Business.Globals.ResourceUrl %>CommonControls/Wysiwyg/Scripts/tiny_mce/tiny_mce_popup.js" type="text/javascript"></script>
<script src="/Common/Js/WPC-Wysiwyg-InternalLink.js" type="text/javascript"></script>
<div class="Wysiwyg-Dialog">
<h1>Insert internal link</h1>

<div id="first-page">
	<uc1:InternalLinkFirstPage id="ctrInternalLinkFirstPage" runat="server" />
</div>

<div class="fullBox formCommands">
	<asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
	<input type="button" value="Close" OnClick="javascript:Cancel()" />
</div>
</div>