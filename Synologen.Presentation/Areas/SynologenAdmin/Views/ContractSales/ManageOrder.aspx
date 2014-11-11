<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.ContractSales.OrderView>" %>
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
                <%if(Model.DisplayCancelButton){ %>
                <div class="formCommands">
					<% using (Html.BeginForm("CancelOrder","ContractSales")) {%>
						<%=Html.AntiForgeryToken() %>
						<%=Html.HiddenFor(x => x.Id) %>
						<input class="btnBig confirm-action" type="submit" value="Makulera" title="Makulera fakturan" />
					<% } %>                    
                </div>
                <% } %>

                <%if(Model.DisplayInvoiceCopyLink){ %>
                <fieldset class="formCommands">
                    <legend>Skapa kreditfaktura</legend>
                    <% using(Html.BeginForm("InvoiceCredit","Report", FormMethod.Get, new{ id = Model.Id })){ %>
                        <label for="CreditInvoiceNumber">Kreditfakturanummer</label>
                        <%=Html.Editor("CreditInvoiceNumber") %>
                        <input type="submit" value="Skapa"/>
                    <%} %>                 
                </fieldset>
                <%} %>
                
                <%if(Model.DisplayInvoiceCopyLink){ %>
                <fieldset class="formCommands">
                    <legend>Skapa fakturakopia</legend>
                    <% using(Html.BeginForm("InvoiceCopy","Report", FormMethod.Get, new{ id = Model.Id })){ %>
                        <input type="submit" value="Skapa"/>
                    <%} %>                    
                </fieldset>                
                <%} %>
                
                <div class="formCommands">
                    <a href="<%=Model.BackUrl %>">&laquo Tillbaka</a>
                </div>								
			</fieldset>											
		</div>
	</div>
</div>	
</asp:Content>