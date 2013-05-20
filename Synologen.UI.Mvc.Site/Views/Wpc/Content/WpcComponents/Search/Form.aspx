<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<%= Html.Action("Form", "WpcSearch", new { area = "WpcSearch", settings = new  { PostToUrl = "results" } }) %>
</asp:Content>