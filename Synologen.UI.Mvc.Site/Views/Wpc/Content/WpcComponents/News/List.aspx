<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<h1>List</h1>
	<%= Html.Action("List", "WpcNews", new { area = "WpcNews", settings = new { MaxNewsDisplayed = "5", DetailPageUrlFormat = "Detail?newsId={0}" } }) %>
</asp:Content>