<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<OrderView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractSales-OrderView-aspx">
	<div class="fullBox">
		<div class="wrap">
			<h2>Order</h2>
			<fieldset>
				<legend>Hantera order</legend>
				<div class="formItem clearLeft">
					<%= Html.LabelFor(x => x.Id)%>
					<span><%=Html.DisplayFor(x => x.Id) %></span>
				</div>
				<div class="formItem clearLeft">
					<%= Html.LabelFor(x => x.Status)%>
					<span><%=Html.DisplayFor(x => x.Status) %></span>
				</div>
				<div class="formItem clearLeft">
					<%= Html.LabelFor(x => x.VISMAInvoiceNumber)%>
					<span><%=Html.DisplayFor(x => x.VISMAInvoiceNumber) %></span>
				</div>

				<div class="formCommands hide-on-print">
					<% using (Html.BeginForm("CancelOrder","ContractSales")) {%>
						<%=Html.AntiForgeryToken() %>
						<%=Html.HiddenFor(x => x.Id) %>
						<a href="<%=Model.BackUrl %>">&laquo Tillbaka</a>
						<span>&nbsp;|&nbsp;</span>
						<%if(Model.DisplayInvoiceCopyLink){ %>
							<a href="<%=Model.InvoiceCopyUrl%>" target="_blank">Visa fakturakopia</a>
						<%} %>
						<span>&nbsp;</span>
						<%if(Model.DisplayCancelButton){ %>
							<input class="btnBig confirm-action" type="submit" value="Makulera" title="Makulera fakturan" />
						<% } %>
					<% } %>
				</div>										
			</fieldset>											
		</div>
	</div>
</div>	
</asp:Content>