<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.ImageManager" Codebehind="ImageManager.ascx.cs" %>
<div class="Files-ImageManager-ascx">
<h2>Image Manager</h2>

<ul class="inlineList">
	<li runat="server" visible="false"><asp:LinkButton ID="btnActualSize" runat="server" Text="Actual size" ToolTip="Actual size" CausesValidation="False" OnClick="btnActualSize_Click" /></li>
	<li runat="server" visible="false"><asp:LinkButton ID="btnBestFit" runat="server" Text="Best fit" ToolTip="Best fit" CausesValidation="False" OnClick="btnBestFit_Click" /></li>
	<li runat="server" visible="false"><asp:LinkButton ID="btnZoomIn" runat="server" Text="Zoom in" ToolTip="Zoom in" CausesValidation="False" OnClick="btnZoomIn_Click" /></li>
	<li runat="server" visible="false"><asp:LinkButton ID="btnZoomOut" runat="server" Text="Zoom out" ToolTip="Zoom out" CausesValidation="False" OnClick="btnZoomOut_Click" /></li>
	<li><asp:LinkButton ID="btnThumbnailCreator" runat="server" Text="Create thumbnail" ToolTip="Create thumbnail" CausesValidation="False" OnClick="btnThumbnailCreator_Click" /></li>
</ul>

<div id="dImagePreview">
<%=m_imageFullPath %>
</div>

<fieldset id="fsCreateThumbnail" runat="server">
<legend>Create a new thumbnail</legend>
<div class="formItem clearLeft">
	<asp:Label ID="lblNewName" runat="server" AssociatedControlID="txtNewImage" SkinID="Long">Thumbnail name</asp:Label>
	<asp:TextBox ID="txtNewImage" runat="server"></asp:TextBox>
</div>

<div class="clearLeft">
	<div class="formItem">
		<asp:Label ID="lblDimensionUnit" runat="server" AssociatedControlID="drpDimensionUnit" SkinID="Long">Dimension unit</asp:Label>
		<asp:DropDownList ID="drpDimensionUnit" runat="server">
			  <asp:ListItem Value="1">Pixel</asp:ListItem>
			  <asp:ListItem Value="2">Percent</asp:ListItem>
		</asp:DropDownList>
	</div>
	<div>
		<div class="formItem">
			<asp:Label ID="lblWidth" runat="server" AssociatedControlID="txtWidth" SkinID="Long">Width</asp:Label>
			<asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
		</div>
		<div class="formItem">
			<asp:Label ID="lblHeight" runat="server" AssociatedControlID="txtHeight" SkinID="Long">Height</asp:Label>
			<asp:TextBox ID="txtHeight" runat="server"></asp:TextBox>
		</div>
	</div>
</div>

<div class="formItem">
	<asp:Label ID="lblResolution" runat="server" AssociatedControlID="txtResolution" SkinID="Long">Resolution</asp:Label>
	<asp:TextBox ID="txtResolution" runat="server"></asp:TextBox>
</div>

<div class="formItem">
<div class="floatLeft">
	<asp:CheckBox ID="chkContstrainPropotions" runat="server" Text="Constrain propotions" />
</div>
<div class="floatLeft">
	<asp:CheckBox ID="chkOverwrite" runat="server" Text="Overwrite" />
</div>
</div>

<div class="formCommands">
	<asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
	<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
</div>
</fieldset>
</div>