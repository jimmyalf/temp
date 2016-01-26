<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.PropertiesManager" Codebehind="PropertiesManager.ascx.cs" %>
<%@ Register Src="~/files/Connections.ascx" TagName="Connections" TagPrefix="uc3" %>
<%@ Register Src="~/files/Additional.ascx" TagName="Additional" TagPrefix="uc2" %>
<%@ Register Src="~/files/General.ascx" TagName="General" TagPrefix="uc1" %>
<div class="Files-PropertiesManager-ascx fullBox">
<h2>Properties</h2>

<asp:PlaceHolder ID="phPropertiesMenu" runat="server" />
<div class="tabsContentContainer clearAfter">
<div class="wrap">
	<uc1:General ID="fleGeneral" runat="server" />
	<uc2:Additional ID="fleAdditional" runat="server" />
	<uc3:Connections ID="fleConnections" runat="server" />
</div>
</div>
</div>