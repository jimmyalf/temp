<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<FrameGlassTypeEditView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameGlassType-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("GlassTypeForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>
