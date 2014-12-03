<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<h1>QuickForm</h1>
	<%= Html.Action("Form", "WpcQuickForm", new { area = "WpcQuickForm", settings = new WpcQuickFormSettings { QuickFormId = 1 } }) %>
</asp:Content>