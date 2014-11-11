<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<h1>Details</h1>
	
	<%= Html.Action("Detail", "WpcNews", new { area = "WpcNews", settings = new WpcNewsDetailSettings()}) %>
</asp:Content>