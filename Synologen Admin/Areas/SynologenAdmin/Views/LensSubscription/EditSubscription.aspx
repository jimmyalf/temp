<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<SubscriptionView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-LensSubscription-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
		
			<% Html.EnableClientValidation(); %>
			<% using (Html.BeginForm("Edit", "LensSubscription")) {%>
			<fieldset>
				<legend>Linsabonnemang</legend>
				<fieldset>
					<legend>Abonemmanginformation</legend>
					<p class="display-item clearLeft">
						<%=Html.LabelFor(x => x.ShopName)%>
						<%=Html.DisplayFor(x => x.ShopName)%>
					</p>
					<p class="display-item clearLeft">
						<%=Html.LabelFor(x => x.Status)%>
						<%=Html.DisplayFor(x => x.Status)%>
					</p>
					<p class="display-item clearLeft">
						<%=Html.LabelFor(x => x.Created)%>
						<%=Html.DisplayFor(x => x.Created)%>
					</p>
					<p class="display-item clearLeft">
						<%=Html.LabelFor(x => x.ConsentStatus)%>
						<%=Html.DisplayFor(x => x.ConsentStatus)%>
					</p>
					<p class="display-item clearLeft">
						<%=Html.LabelFor(x => x.ConsentDate)%>
						<%=Html.DisplayFor(x => x.ConsentDate)%>
					</p>
					<p class="formItem clearLeft">
						<%=Html.LabelFor(x => x.AccountNumber)%>
						<%=Html.EditorFor(x => x.AccountNumber)%>
						<%=Html.ValidationMessageFor(x => x.AccountNumber) %>
					</p>				
					<p class="formItem">
						<%=Html.LabelFor(x => x.ClearingNumber)%>
						<%=Html.EditorFor(x => x.ClearingNumber)%>
						<%=Html.ValidationMessageFor(x => x.ClearingNumber) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.MonthlyAmount)%>
						<%=Html.EditorFor(x => x.MonthlyAmount)%>
						<%=Html.ValidationMessageFor(x => x.ClearingNumber) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.SubscriptionNotes)%>
						<span><%=Html.TextAreaFor(x => x.SubscriptionNotes)%></span>
					</p>	
					<p class="formItem">
						<%=Html.LabelFor(x => x.FirstName)%>
						<%= Html.EditorFor(x => x.FirstName) %>
						<%=Html.ValidationMessageFor(x => x.FirstName) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.LastName)%>
						<%= Html.EditorFor(x => x.LastName) %>
						<%=Html.ValidationMessageFor(x => x.LastName) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.PersonalIdNumber)%>
						<%=Html.EditorFor(x => x.PersonalIdNumber)%>
						<%=Html.ValidationMessageFor(x => x.PersonalIdNumber) %>
					</p>					
					<p class="formItem">
						<%=Html.LabelFor(x => x.Phone)%>
						<%=Html.EditorFor(x => x.Phone)%>
						<%=Html.ValidationMessageFor(x => x.Phone) %>
					</p>				
					<p class="formItem">
						<%=Html.LabelFor(x => x.MobilePhone)%>
						<%=Html.EditorFor(x => x.MobilePhone)%>
						<%=Html.ValidationMessageFor(x => x.MobilePhone) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.Email)%>
						<%=Html.EditorFor(x => x.Email)%>
						<%=Html.ValidationMessageFor(x => x.Email) %>
					</p>		
					<p class="formItem">
						<%=Html.LabelFor(x => x.AddressLineOne)%>
						<%=Html.EditorFor(x => x.AddressLineOne)%>
						<%=Html.ValidationMessageFor(x => x.AddressLineOne) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.AddressLineTwo)%>
						<%=Html.EditorFor(x => x.AddressLineTwo)%>
						<%=Html.ValidationMessageFor(x => x.AddressLineTwo) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.PostalCode)%>
						<%=Html.EditorFor(x => x.PostalCode)%>
						<%=Html.ValidationMessageFor(x => x.PostalCode) %>
					</p>
					<p class="formItem">
						<%=Html.LabelFor(x => x.City)%>
						<%=Html.EditorFor(x => x.City)%>
						<%=Html.ValidationMessageFor(x => x.City) %>
					</p>
					<p class="display-item">
						<%=Html.LabelFor(x => x.Country)%>
						<%=Html.DisplayFor(x => x.Country)%>
					</p>
					<p class="formItem clearLeft">
						<%=Html.LabelFor(x => x.CustomerNotes)%>
						<span ><%=Html.TextAreaFor(x => x.CustomerNotes)%></span>
					</p>
					<p class="formItem formCommands">
						<%= Html.AntiForgeryToken() %>
						<%= Html.HiddenFor(x => x.CustomerId) %>
						<input type="submit" value="Spara abonnemang" class="btnBig" />
					</p>	
				</fieldset>									
				<% } %>
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