<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternalLink.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.InternalLink"  %>
<%@ Register Src="InternalLinkFirstPage.ascx" TagName="InternalLinkFirstPage" TagPrefix="uc1" %>
<%@ Register Src="InternalLinkSecondPage.ascx" TagName="InternalLinkSecondPage" TagPrefix="uc2" %>
<%@ Register Src="~/Common/StyleSheetLoader.ascx" TagName="SSLoader" TagPrefix="userControl" %>
<%@ Register Src="~/Common/JavaScriptLoader.ascx" TagName="JSLoader" TagPrefix="userControl" %>
<userControl:SSLoader ID="SSLoader" runat="server" />
<userControl:JSLoader ID="JSLoader" runat="server" />
<link rel="stylesheet" type="text/css" href="/Common/Css/Dialogs.css" media="screen" />
<script src="/Common/Js/WPC-Wysiwyg-InternalLink.js" type="text/javascript"></script>
<div class="Wysiwyg-Dialog">
<div id="dCompNavigation">
	<ul>
		<li><a href="javascript:ShowFirstPage();"><span>LINK</span></a></li>
		<li><a href="javascript:ShowSecondPage();"><span>PROPERTIES</span></a></li>
	</ul>
</div>
<h1>Insert internal link</h1>

<div id="first-page">
	<uc1:InternalLinkFirstPage id="ctrInternalLinkFirstPage" runat="server" />
</div>
<div id="second-page" style="display:none;">
	<uc2:InternalLinkSecondPage id="ctrInternlaLinkSecondPage" runat="server" />
</div>

<div class="fullBox formCommands">
	<asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
	<input type="button" value="Close" OnClick="javascript:Cancel()" />
	<input type="button" value="Remove" OnClick="javascript:Remove()" />
</div>
</div>