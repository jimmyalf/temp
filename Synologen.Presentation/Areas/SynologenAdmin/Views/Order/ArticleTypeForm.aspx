<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<ArticleTypeFormView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-Order-Category-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.EnableClientValidation(); %>
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<%if(Model.IsCreate){ %>
				<legend>Skapa ny artikeltyp</legend>
				<% } else{%>
				<legend>Redigera artikeltyp</legend>
				<% } %>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.CategoryId) %>
						<%= Html.DropDownListFor(x => x.CategoryId, Model.Categories, optionLabel: "-- Välj Artikelkategori --") %>
						<%= Html.ValidationMessageFor(x => x.CategoryId) %>
					</p>
					<p class="formItem clearLeft">
						<%=Html.LabelFor(x => x.Name)%>
						<%=Html.EditorFor(x => x.Name)%>
					</p>
					<p class="formItem formCommands">
						<%= Html.AntiForgeryToken() %>
						<%= Html.HiddenFor(x => x.Id) %>
						<input type="submit" value="Spara artikeltyp" class="btnBig" />
					</p>	
					<p class="display-item clearLeft">
						<a href='<%= Url.Action("ArticleTypes") %>'>Tillbaka till artikeltyper &raquo;</a>
					</p>
				</fieldset>									
				<% } %>		
		</div>
	</div>
</div>	
</asp:Content>