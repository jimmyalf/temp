<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<FtpProfileView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractSales-EditFtpProfile-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("FtpProfileForm", Model); %>			
		</div>
	</div>
</div>	
</asp:Content>