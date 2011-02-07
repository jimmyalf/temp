<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<TransactionArticleModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-TransactionArticle-Add">
	<div class="fullBox">
		<div class="wrap">	
			<% Html.RenderPartial("TransactionArticleForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>
