<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.FrameSupplierEditView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameSupplier-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("SupplierForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>
