<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<ShopGroupFormView>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("SynologenSubMenu"); %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-Synologen-ShopGroup-Form">
	<div class="fullBox">
		<div class="wrap">
			<% Html.EnableClientValidation(); %>
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<%if(Model.IsCreate){ %>
				<legend>Skapa ny butikgrupp</legend>
				<% } else{%>
				<legend>Redigera butikgrupp</legend>
				<% } %>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.Name)%>
						<%= Html.EditorFor(x => x.Name)%>
						<%= Html.ValidationMessageFor(x => x.Name) %>
					</p>
					<p class="formItem formCommands">
						<%= Html.AntiForgeryToken() %>
						<%= Html.HiddenFor(x => x.Id) %>
						<input type="submit" value="Spara butikgrupp" class="btnBig" />
					</p>
					<p class="display-item clearLeft">
						<a href='<%= Url.Action("ShopGroups") %>'>Tillbaka till butikgrupper &raquo;</a>
					</p>
			</fieldset>					
			<% } %>
			<p></p>
			<fieldset>
				<legend>Butiker i gruppen</legend>
					<table class="formItem striped">
						<thead>
							<tr><th>Id</th><th>Nummer</th><th>Namn</th></tr>
						</thead>
						<tbody>
						<% foreach (var shop in Model.Shops){%>
							<tr>
								<td><%=shop.Id %></td>
								<td><%=shop.Number %></td>
								<td><%=shop.Name %></td>
							</tr>
						<%} %>
						</tbody>
					</table>
				</fieldset>
		</div>
	</div>
</div>	
</asp:Content>