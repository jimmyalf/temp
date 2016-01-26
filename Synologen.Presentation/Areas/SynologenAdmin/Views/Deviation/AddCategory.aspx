<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.Deviation.CategoryFormView>" %>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
    <% Html.RenderPartial("DeviationSubMenu"); %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameBrand-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("_CategoryForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>

