<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<SubscriptionView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-LensSubscription-View-aspx">
	<div class="fullBox">
		<div class="wrap">
			<fieldset>
				<legend>Linsabonnemang</legend>
				<fieldset>
					<legend>Abonemmanginformation</legend>
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.ShopName) %>
						<%= Html.DisplayFor(x => x.ShopName) %>
					</p>					
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.AccountNumber) %>
						<%= Html.DisplayFor(x => x.AccountNumber) %>
					</p>				
					<p class="display-item">
						<%= Html.LabelFor(x => x.ClearingNumber) %>
						<%= Html.DisplayFor(x => x.ClearingNumber) %>
					</p>
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.Status) %>
						<%= Html.DisplayFor(x => x.Status) %>
					</p>
					<p class="display-item">
						<%= Html.LabelFor(x => x.Activated) %>
						<%= Html.DisplayFor(x => x.Activated) %>
					</p>				
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.Created) %>
						<%= Html.DisplayFor(x => x.Created) %>
					</p>
					<p class="display-item">
						<%= Html.LabelFor(x => x.MonthlyAmount) %>
						<%= Html.DisplayFor(x => x.MonthlyAmount) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.SubscriptionNotes) %>
						<span><%= Html.DisplayFor(x => x.SubscriptionNotes) %></span>
					</p>	
				</fieldset>
				<fieldset>
					<legend>Kunduppgifter</legend>
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.CustomerName) %>
						<%= Html.DisplayFor(x => x.CustomerName) %>
					</p>
					<p class="display-item">
						<%= Html.LabelFor(x => x.PersonalIdNumber) %>
						<%= Html.DisplayFor(x => x.PersonalIdNumber) %>
					</p>					
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.Phone) %>
						<%= Html.DisplayFor(x => x.Phone) %>
					</p>				
					<p class="display-item">
						<%= Html.LabelFor(x => x.MobilePhone) %>
						<%= Html.DisplayFor(x => x.MobilePhone) %>
					</p>
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.Email) %>
						<%= Html.DisplayFor(x => x.Email) %>
					</p>		
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.AddressLineOne) %>
						<%= Html.DisplayFor(x => x.AddressLineOne) %>
					</p>
					<p class="display-item">
						<%= Html.LabelFor(x => x.AddressLineTwo) %>
						<%= Html.DisplayFor(x => x.AddressLineTwo) %>
					</p>
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.PostalCode) %>
						<%= Html.DisplayFor(x => x.PostalCode) %>
					</p>
					<p class="display-item">
						<%= Html.LabelFor(x => x.City) %>
						<%= Html.DisplayFor(x => x.City) %>
					</p>
					<p class="display-item clearLeft">
						<%= Html.LabelFor(x => x.Country) %>
						<%= Html.DisplayFor(x => x.Country) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.CustomerNotes) %>
						<span ><%= Html.DisplayFor(x => x.CustomerNotes) %></span>
					</p>										
				</fieldset>
					<fieldset>
						<legend><%=Html.GetDisplayNameFor(x => x.ErrorList) %></legend>
						<p class="display-item clearLeft">						
							<table class="striped">
								<tr class="header"><th>Datum</th><th>Typ av fel</th><th>Hanterad</th></tr>
								<%foreach (var item in Model.ErrorList) {%>
								<tr class="gridrow">
									<td><%= item.CreatedDate %></td>
									<td><%= item.Type %></td>
									<td><%= item.HandledDate %></td>
								</tr>
								<%}%>
							</table>
						</p>						
					</fieldset>				
					<fieldset>
						<legend><%=Html.GetDisplayNameFor(x => x.TransactionList) %></legend>
						<p class="display-item clearLeft">						
							<table class="striped">
								<tr class="header"><th>Datum</th><th>Insättningar</th><th>Utbetalningar</th><th>Orsak</th></tr>
								<%foreach (var item in Model.TransactionList) {%>
								<tr class="gridrow">
									<td><%= item.Date %></td>
									<td><%= item.DepositAmount %></td>
									<td><%= item.WithdrawalAmount %></td>
									<td><%= item.Reason %></td>
								</tr>
								<%}%>
							</table>
						</p>						
					</fieldset>				
				<p class="display-item clearLeft">
					<a href='<%= Url.Action("Index") %>'>Tillbaka till abonnemang &raquo;</a>
				</p>
			</fieldset>				
		</div>
	</div>
</div>	
</asp:Content>