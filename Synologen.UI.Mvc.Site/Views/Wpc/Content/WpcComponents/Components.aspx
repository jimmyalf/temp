<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<h1>Components</h1>
	<%= Html.Action("Menu",	"WpcContent", new { area = "WpcContent", settings = new { Class = "component-navigation", ShowRootLevel = false, ShowDefaultPage = false, StartAtLevel = 1, StopAtLevel = 1 } }) %>
</asp:Content>