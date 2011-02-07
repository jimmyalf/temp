<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderView.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrders.OrderView" %>
<%if(Model.DisplayOrder) { %>
<div id="synologen-view-frame-order" class="synologen-control">
<div class="frame-order-view">
	<p><label>Märke: </label><span><%#Model.FrameBrand%></span></p>
	<p><label>Båge: </label><span><%#Model.FrameName%></span></p>
	<p><label>Bågfärg: </label><span><%#Model.FrameColor%></span></p>
	<p><label>Båge Artnr: </label><span><%#Model.FrameArticleNumber%></span></p>	
	<p><label>Glastyp: </label><span><%#Model.GlassTypeName%></span></p>
	<ul class="parameter-list">
		<li class="parameter-item template"><label>&nbsp;</label><span>Höger:</span><span>Vänster:</span></li>
		<li class="parameter-item sphere"><label>Sfär</label><span><%#Model.SphereRight%></span><span><%#Model.SphereLeft%></span></li>
		<li class="parameter-item cylinder"><label>Cylinder</label><span><%#Model.CylinderRight%></span><span><%#Model.CylinderLeft%></span></li>
		<li class="parameter-item axis"><label>Axel</label><span><%#Model.AxisSelectionRight%></span><span><%#Model.AxisSelectionLeft%></span></li>
		<li class="parameter-item addition"><label>Addition</label><span><%#Model.AdditionRight%></span><span><%#Model.AdditionLeft%></span></li>
	</ul>
	<ul class="parameter-list">
		<li class="parameter-item template"><label>&nbsp;</label><span>Höger:</span><span>Vänster:</span></li>
		<li class="parameter-item pd"><label>PD</label><span><%#Model.PupillaryDistanceRight%></span><span><%#Model.PupillaryDistanceLeft%></span></li>
		<li class="parameter-item height"><label>Höjd</label><span><%#Model.HeightRight%></span><span><%#Model.HeightLeft%></span></li>	
	</ul>	
	<p><label>Butik: </label><span><%#Model.ShopName%></span></p>
	<p><label>Butikort: </label><span><%#Model.ShopCity%></span></p>
	<p><label>Referens: </label><span><%#Model.Reference%></span></p>
	<p><label>Beställning skapad: </label><span><%#Model.CreatedDate%></span></p>
	<p><label>Beställning skickad: </label><span><%#Model.SentDate%></span></p>	
</div>
<%} %>
</div>
<%if(!Model.OrderHasBeenSent && Model.DisplayOrder) { %>
<a href="<%#Model.EditPageUrl%>">Fortsätt redigera</a>
&nbsp;|&nbsp;
<asp:Button ID="btnSend" runat="server" Text="Skicka beställning" />
<%} %>

<%if (Model.OrderDoesNotExist) { %>
<p>Begärd beställning kunde inte hittas. Var god kontakta systemadministratören.</p>
<%} %>

<%if (Model.UserDoesNotHaveAccessToThisOrder) { %>
<p>Rättighet för att se beställning saknas. Var god kontakta systemadministratören.</p>
<%} %>

<%if(Model.ShopDoesNotHaveAccessToFrameOrders){ %>
<p>Rättighet för att se beställning kan inte medges. Var god kontakta systemadministratören.</p>
<%} %>
