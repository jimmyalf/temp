<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<FrameEditView>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-Frame-Add-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("FrameForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>