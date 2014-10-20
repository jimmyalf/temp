<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<h1>BasicLogin</h1>
	
	<h2>Example</h2>
	<%= Html.Action("BasicLogin", "WpcContent", new { area = "WpcContent", settings = new {}}) %>
</asp:Content>