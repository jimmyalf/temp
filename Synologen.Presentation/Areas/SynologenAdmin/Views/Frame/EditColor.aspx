<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<FrameColorEditView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameColor-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("ColorForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>
