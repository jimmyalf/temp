<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Component.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.Component" %>
<%@ Register Src="~/Common/StyleSheetLoader.ascx" TagName="SSLoader" TagPrefix="userControl" %>
<%@ Register Src="~/Common/JavaScriptLoader.ascx" TagName="JSLoader" TagPrefix="userControl" %>
<userControl:SSLoader ID="SSLoader" runat="server" />
<userControl:JSLoader ID="JSLoader" runat="server" />
<link rel="stylesheet" type="text/css" href="/Common/Css/Dialogs.css" media="screen" />
<script src="/Common/Js/WPC-Wysiwyg-Component.js" type="text/javascript"></script>
<asp:PlaceHolder ID="phScript" runat="server" />
<div class="fullBox">
	<div class="wrap">
	<h1>Insert component</h1>

		<div class="formItem">
			<label class="labelLong">Choose the component</label>
			<asp:DropDownList ID="drpComponents" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpComponents_SelectedIndexChanged" />
		</div>
		<div>
			<iframe id="cmpFrame" name="cmpFrame" height="250" width="100%" scrolling="auto" src="<%=ComponentUrl%>"></iframe>
		</div>
		<div class="formCommands">
			<asp:Button ID="btnInsert" runat="server" Text="Insert" SkinID="Big" OnClick="btnInsert_Click" Visible="false"/>
			<input type="button" value="Close" OnClick="javascript:Cancel()" />
		</div>
	</div>
</div>
