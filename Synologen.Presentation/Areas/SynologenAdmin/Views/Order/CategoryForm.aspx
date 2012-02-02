<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<CategoryFormView>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-Order-Category-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.EnableClientValidation(); %>
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<%if(Model.IsCreate){ %>
				<legend>Skapa ny artikelkategori</legend>
				<% } else{%>
				<legend>Redigera artikelkategori</legend>
				<% } %>
				
					<p class="formItem clearLeft">
						<%=Html.LabelFor(x => x.Name)%>
						<%=Html.EditorFor(x => x.Name)%>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.Active)%>
						<%= Html.EditorFor(x => x.Active)%>
					</p>
					<p class="formItem formCommands">
						<%= Html.AntiForgeryToken() %>
						<%= Html.HiddenFor(x => x.Id) %>
						<input type="submit" value="Spara artikelkategori" class="btnBig" />
					</p>	
					<p class="display-item clearLeft">
						<a href='<%= Url.Action("Categories") %>'>Tillbaka till artikelkategorier &raquo;</a>
					</p>
				</fieldset>									
				<% } %>		
		</div>
	</div>
</div>	
</asp:Content>