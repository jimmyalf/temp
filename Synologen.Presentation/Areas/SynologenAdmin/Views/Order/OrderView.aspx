<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.Order.OrderView>" %>
<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-Order-View-aspx">
	<div class="fullBox">
		<div class="wrap">
		<fieldset>
			<legend>Beställning <%= Model.OrderId %></legend>
			<fieldset>
				<legend>Kundinformation</legend>
				<p><%= Html.LabelFor(x => x.Name) %><%= Html.DisplayFor(x => x.Name) %></p>
				<p><%= Html.LabelFor(x => x.PersonalIdNumber) %><%= Html.DisplayFor(x => x.PersonalIdNumber) %></p>
				<p><%= Html.LabelFor(x => x.Email) %><%= Html.DisplayFor(x => x.Email) %></p>
				<p><%= Html.LabelFor(x => x.MobilePhone) %><%= Html.DisplayFor(x => x.MobilePhone) %></p>
				<p><%= Html.LabelFor(x => x.Telephone) %><%= Html.DisplayFor(x => x.Telephone) %></p>
				<p><%= Html.LabelFor(x => x.Address) %><%= Html.DisplayFor(x => x.Address.AddressLineOne) %><br/><%= Html.DisplayFor(x => x.Address.AddressLineTwo) %></p>
				<p><%= Html.LabelFor(x => x.Article) %><%= Html.DisplayFor(x => x.Article) %></p>
			</fieldset>
			<fieldset>
				<legend>Parametrar</legend>
				<table class="formItem striped">
					<tr class="header"><th>Parameter</th><th>Höger</th><th>Vänster</th></tr>
					<tr>
						<td><%=Html.GetDisplayName(x => x.Power) %></td>
						<td><%=Html.DisplayFor(x => x.Power.Right) %></td>
						<td><%=Html.DisplayFor(x => x.Power.Left) %></td>
					</tr>
					<tr>
						<td><%=Html.GetDisplayName(x => x.Addition) %></td>
						<td><%=Html.DisplayFor(x => x.Addition.Right) %></td>
						<td><%=Html.DisplayFor(x => x.Addition.Left) %></td>
					</tr>
					<tr>
						<td><%=Html.GetDisplayName(x => x.BaseCurve) %></td>
						<td><%=Html.DisplayFor(x => x.BaseCurve.Right) %></td>
						<td><%=Html.DisplayFor(x => x.BaseCurve.Left) %></td>
					</tr>
					<tr>
						<td><%=Html.GetDisplayName(x => x.Diameter) %></td>
						<td><%=Html.DisplayFor(x => x.Diameter.Right) %></td>
						<td><%=Html.DisplayFor(x => x.Diameter.Left) %></td>
					</tr>
					<tr>
						<td><%=Html.GetDisplayName(x => x.Axis) %></td>
						<td><%=Html.DisplayFor(x => x.Axis.Right) %></td>
						<td><%=Html.DisplayFor(x => x.Axis.Left) %></td>
					</tr>
					<tr>
						<td><%=Html.GetDisplayName(x => x.Cylinder) %></td>
						<td><%=Html.DisplayFor(x => x.Cylinder.Right) %></td>
						<td><%=Html.DisplayFor(x => x.Cylinder.Left) %></td>
					</tr>
				</table>
			</fieldset>
			<fieldset>
				<legend>Abonemmangsinformation</legend>
				<p><%= Html.LabelFor(x => x.DeliveryOption)%><%= Html.DisplayFor(x => x.DeliveryOption)%></p>
				<p><%= Html.LabelFor(x => x.ProductPrice)%><%= Html.DisplayFor(x => x.ProductPrice)%></p>
				<p><%= Html.LabelFor(x => x.FeePrice)%><%= Html.DisplayFor(x => x.FeePrice)%></p>
				<p><%= Html.LabelFor(x => x.Monthly)%><%= Html.DisplayFor(x => x.Monthly)%></p>
				<p><%= Html.LabelFor(x => x.NumerOfWithdrawals)%><%= Html.DisplayFor(x => x.NumerOfWithdrawals)%></p>
				<p><%= Html.LabelFor(x => x.NumberOfPerformedWithdrawals)%><%= Html.DisplayFor(x => x.NumberOfPerformedWithdrawals)%></p>
				<p><%= Html.LabelFor(x => x.TotalWithdrawal)%><%= Html.DisplayFor(x => x.TotalWithdrawal)%></p>
				<p><%= Html.LabelFor(x => x.Reference)%><%= Html.DisplayFor(x => x.Reference)%></p>
			</fieldset>
			<p class="display-item clearLeft">
				<a href='<%= Url.Action("Orders") %>'>Tillbaka till beställningar &raquo;</a>
			</p>
			</fieldset>
		</div>
	</div>
</div>	
</asp:Content>