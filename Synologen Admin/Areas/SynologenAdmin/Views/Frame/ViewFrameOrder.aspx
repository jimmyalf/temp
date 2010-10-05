<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<FrameOrderView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-FrameOrder-View-aspx">
	<div class="fullBox">
		<div class="wrap">
			<fieldset>
				<legend>Beställning</legend>
				
				<p class="display-item ">
					<%= Html.LabelFor(x => x.Id) %>
					<%= Html.DisplayFor(x => x.Id) %>
				</p>
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.Frame) %>
					<span><%= Html.DisplayFor(x => x.Frame) %></span>
				</p>
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.FrameArticleNumber) %>
					<%= Html.DisplayFor(x => x.FrameArticleNumber) %>
				</p>
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.GlassType) %>
					<%= Html.DisplayFor(x => x.GlassType) %>
				</p>	
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.Shop) %>
					<%= Html.DisplayFor(x => x.Shop) %>
				</p>
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.ShopCity) %>
					<%= Html.DisplayFor(x => x.ShopCity) %>
				</p>
				<fieldset style="clear:both">
					<legend>Parametrar</legend>
					<p class="display-item eyeparameter template">
						<label>&nbsp;</label>
						<span>Höger</span>
						<span>Vänster</span>
					</p>									
					<p class="display-item eyeparameter sphere">
						<%= Html.DisplayFor(x => x.Sphere,"EyeParameterView") %>
					</p>
					<p class="display-item eyeparameter cylinder">
						<%= Html.DisplayFor(x => x.Cylinder,"EyeParameterView") %>
					</p>					
					<p class="display-item eyeparameter axis">
						<%= Html.DisplayFor(x => x.Axis,"EyeParameterView") %>
					</p>
					<% if(Model.Addition != null){ %>
					<p class="display-item eyeparameter addition">
						<%= Html.DisplayFor(x => x.Addition,"EyeParameterView") %>
					</p>
					<%} %>
					<p class="display-item eyeparameter pd">
						<%= Html.DisplayFor(x => x.PupillaryDistance,"EyeParameterView") %>
					</p>					
					<% if(Model.Height != null){ %>
					<p class="display-item eyeparameter height">
						<%= Html.DisplayFor(x => x.Height,"EyeParameterView") %>
					</p>
					<%} %>																		
				</fieldset>					
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.Created) %>
					<%= Html.DisplayFor(x => x.Created) %>
				</p>				
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.Sent) %>
					<%= Html.DisplayFor(x => x.Sent) %>
				</p>								
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.Notes) %>
					<br />
					<%= Html.DisplayFor(x => x.Notes) %>
				</p>					
				<p class="display-item clearLeft">
					<%= Html.ActionLink("Tillbaka till beställningar", "FrameOrders") %>
				</p>
			</fieldset>				
		</div>
	</div>
</div>	
</asp:Content>