<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FrameOrderView.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrderView" %>
<%if(Model.DisplayOrder) { %>
<div class="frame-order-view">
	<p><label>Cylinder Vänster: </label><span><%#Model.CylinderLeft%></span></p>
	<p><label>Cylinder Höger: </label><span><%#Model.CylinderRight%></span></p>

	<p><label>Sfär Vänster: </label><span><%#Model.SphereLeft%></span></p>
	<p><label>Sfär Höger: </label><span><%#Model.SphereRight%></span></p>

	<p><label>PD Vänster: </label><span><%#Model.PupillaryDistanceLeft%></span></p>
	<p><label>PD Höger: </label><span><%#Model.PupillaryDistanceRight%></span></p>

	<p><label>Addition Vänster: </label><span><%#Model.AdditionLeft%></span></p>
	<p><label>Addition Höger: </label><span><%#Model.AdditionRight%></span></p>

	<p><label>Höjd Vänster: </label><span><%#Model.HeightLeft%></span></p>
	<p><label>Höjd Höger: </label><span><%#Model.HeightRight%></span></p>

	<p><label>Axl Vänster: </label><span><%#Model.AxisSelectionLeft%></span></p>
	<p><label>Axel Höger: </label><span><%#Model.AxisSelectionRight%></span></p>

	<p><label>Båge: </label><span><%#Model.FrameName%></span></p>
	<p><label>Båge Artnr: </label><span><%#Model.FrameArticleNumber%></span></p>
	<p><label>Bågfärg: </label><span><%#Model.FrameColor%></span></p>
	<p><label>Märke: </label><span><%#Model.FrameBrand%></span></p>
	<p><label>Glastyp: </label><span><%#Model.GlassTypeName%></span></p>
	<p><label>Butik: </label><span><%#Model.ShopName%></span></p>
	<p><label>Butikort: </label><span><%#Model.ShopCity%></span></p>
	<p><label>Beställning skapad: </label><span><%#Model.CreatedDate%></span></p>
	<p><label>Beställning skickad: </label><span><%#Model.SentDate%></span></p>
	<p><label>Anteckningar: </label><span><%#Model.Notes%></span></p>
</div>
<%} %>

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
