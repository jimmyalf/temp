<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.master" Inherits="System.Web.Mvc.ViewPage<FrameBrandEditView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameBrand-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("FrameBrandForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>
