<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<SettlementView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractSales-ViewSettlement-aspx">
	<div class="fullBox">
		<div class="wrap">
			<h2>Utbetalning</h2>
			<fieldset>
				<legend>Utbetalningsdetaljer</legend>
				<div class="formItem clearLeft">
					<div><%= Html.LabelFor(x => x.Id)%>
					<span><%=Html.DisplayFor(x => x.Id) %></span></div>
					<div><%= Html.LabelFor(x => x.CreatedDate)%>
					<span><%=Html.DisplayFor(x => x.CreatedDate) %></span></div>
					<div><%= Html.LabelFor(x => x.Period)%>
					<span><%=Html.DisplayFor(x => x.Period) %></span></div>
				</div>
				<div class="formItem">
					<div><%= Html.LabelFor(x => x.SumAmountIncludingVAT)%>
					<span><%=Html.DisplayFor(x => x.SumAmountIncludingVAT) %></span></div>
					<div><%= Html.LabelFor(x => x.SumAmountExcludingVAT)%>
					<span><%=Html.DisplayFor(x => x.SumAmountExcludingVAT) %></span></div>
				</div>		
				<div class="formCommands hide-on-print">
					<input class="btnBig" type="button" OnClick="window.location='<%=Url.Action("Settlements") %>';" value="Tillbaka">
					<input class="btnBig" type="button" OnClick="window.print();return false;" value="Skriv ut">
				</div>										
			</fieldset>
			<br />
			<div>						
				<table class="striped">
					<tr class="header">
						<th class="wide-column">Butik</th>
						<th>Bankgiro</th>
						<th class="hide-on-print">Antal fakturor</th>
						<th class="hide-on-print">Utbetalas exkl moms</th>
						<th>Utbetalas inkl moms</th>
					</tr>
					<%foreach (var item in Model.SettlementItems) {%>
					<tr>
						<td><%= item.ShopDescription %></td>
						<td><%= item.BankGiroNumber %></td>
						<td class="hide-on-print"><%= item.NumberOfContractSalesInSettlement %></td>
						<td class="hide-on-print"><%= item.SumAmountExcludingVAT %></td>
						<td><%=item.SumAmountIncludingVAT %></td>
					</tr>
					<%}%>
				</table>
			</div>												
		</div>
	</div>
</div>	
</asp:Content>