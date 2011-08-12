<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<ArticleView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractSales-EditArticle-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("ArticleForm", Model); %>									
		</div>
	</div>
</div>	
</asp:Content>