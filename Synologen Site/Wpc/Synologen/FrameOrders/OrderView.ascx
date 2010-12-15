<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderView.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrders.OrderView" %>
<%if(Model.DisplayOrder) { %>
<div id="synologen-view-frame-order" class="synologen-control">
<div class="frame-order-view">
	<p><label>M�rke: </label><span><%#Model.FrameBrand%></span></p>
	<p><label>B�ge: </label><span><%#Model.FrameName%></span></p>
	<p><label>B�gf�rg: </label><span><%#Model.FrameColor%></span></p>
	<p><label>B�ge Artnr: </label><span><%#Model.FrameArticleNumber%></span></p>	
	<p><label>Glastyp: </label><span><%#Model.GlassTypeName%></span></p>
	<ul class="parameter-list">
		<li class="parameter-item template"><label>&nbsp;</label><span>H�ger:</span><span>V�nster:</span></li>
		<li class="parameter-item sphere"><label>Sf�r</label><span><%#Model.SphereRight%></span><span><%#Model.SphereLeft%></span></li>
		<li class="parameter-item cylinder"><label>Cylinder</label><span><%#Model.CylinderRight%></span><span><%#Model.CylinderLeft%></span></li>
		<li class="parameter-item axis"><label>Axel</label><span><%#Model.AxisSelectionRight%></span><span><%#Model.AxisSelectionLeft%></span></li>
		<li class="parameter-item addition"><label>Addition</label><span><%#Model.AdditionRight%></span><span><%#Model.AdditionLeft%></span></li>
	</ul>
	<ul class="parameter-list">
		<li class="parameter-item template"><label>&nbsp;</label><span>H�ger:</span><span>V�nster:</span></li>
		<li class="parameter-item pd"><label>PD</label><span><%#Model.PupillaryDistanceRight%></span><span><%#Model.PupillaryDistanceLeft%></span></li>
		<li class="parameter-item height"><label>H�jd</label><span><%#Model.HeightRight%></span><span><%#Model.HeightLeft%></span></li>	
	</ul>	
	<p><label>Butik: </label><span><%#Model.ShopName%></span></p>
	<p><label>Butikort: </label><span><%#Model.ShopCity%></span></p>
	<p><label>Referens: </label><span><%#Model.Reference%></span></p>
	<p><label>Best�llning skapad: </label><span><%#Model.CreatedDate%></span></p>
	<p><label>Best�llning skickad: </label><span><%#Model.SentDate%></span></p>	
</div>
<%} %>
</div>
<%if(!Model.OrderHasBeenSent && Model.DisplayOrder) { %>
<a href="<%#Model.EditPageUrl%>">Forts�tt redigera</a>
&nbsp;|&nbsp;
<asp:Button ID="btnSend" runat="server" Text="Skicka best�llning" />
<%} %>

<%if (Model.OrderDoesNotExist) { %>
<p>Beg�rd best�llning kunde inte hittas. Var god kontakta systemadministrat�ren.</p>
<%} %>

<%if (Model.UserDoesNotHaveAccessToThisOrder) { %>
<p>R�ttighet f�r att se best�llning saknas. Var god kontakta systemadministrat�ren.</p>
<%} %>

<%if(Model.ShopDoesNotHaveAccessToFrameOrders){ %>
<p>R�ttighet f�r att se best�llning kan inte medges. Var god kontakta systemadministrat�ren.</p>
<%} %>
