<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<SettlementListView>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-LensSubscriptions">
	<div class="fullBox">
		<div class="wrap">
			<h2>Butiksutbetalningar</h2>
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<div class="formItem">
		            <label class="labelLong">Antal ordrar redo för utbetalning: <%=Model.NumberOfContractSalesReadyForInvocing %></label>
		        </div>
				<div class="formCommands">
					<input type="submit" value="Skapa utbetalning" class="btnBig" />
				</div>				
			</fieldset>
			<% } %>
			<br />
			<div>
				<% Html.WpcGrid(Model.Settlements).Columns( column => 
					{
 						column.For(x => x.Id).Named("Id").Sortable(false);;
 						column.For(x => x.CreatedDate).Named("Skapad").Sortable(false);
					    column.For(x => x.NumberOfContractSalesInSettlement).Named("Antal fakturor").Sortable(false);;
					    column.For(x => x).Named("Fakturor").Action(settlement => { %>				    
							<td><a href="/Components/Synologen/Orders.aspx?settlementId=<%=settlement.Id%>">Visa fakturor</a></td>
						<%}).SetAsWpcControlColumn("Fakturor");
						column.For(x => Html.ActionLink("Visa detaljer", "ViewSubscription", "LensSubscription", new {id = x.Id}, new object()))
							.SetAsWpcControlColumn("Detaljer");
 					}).Empty("Inga utbetalningar i databasen.").Render();
				%>
			</div>
		</div>     						
	</div>				
</div>
</asp:Content>