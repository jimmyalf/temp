<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.SearchShopView>" %>

<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
<script type="text/javascript">
    function initialize() {
        var myOptions = {
            zoom: 8,
            center: new google.maps.LatLng(-34.397, 150.644),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
        var bounds = new google.maps.LatLngBounds();

        <% foreach (var item in ViewData.Model.Shops) { %>
        var latlng = AddElementToMap(map, <%= item.Latitude %>, <%= item.Longitude %>, "<%= item.Description %>", "<%= item.Name %>");
        bounds.extend(latlng);
        <% } %>

        map.fitBounds(bounds);
    }

    google.maps.event.addDomListener(window, 'load', initialize);

    function AddElementToMap(map, lat, lng, desc, name)
    {
        var latlng = new google.maps.LatLng(lat, lng);
        var infowindow = new google.maps.InfoWindow({ content: desc });
        var marker = new google.maps.Marker({ position: latlng, map: map, title: name });
        google.maps.event.addListener(marker, 'click', function() { infowindow.open(map, marker); });
        return latlng;
    }
</script>

<div id="map_canvas" style="width: 800px; height: 600px"></div>
<!--Side Content-->
<aside>
<!--Side Navigation-->
	<nav>
	</nav>
<!--Store Finder-->
	<section class="find-store">
		<h1>Hitta butik</h1>
		<p><b>Vi finns i hela landet med över 120 anslutna butiker.</b></p><p>Skriv in namn på butik, postnummer eller ort nedan för att hitta en Synolog nära dig.</p>
        <form action="/" method="get">
			<fieldset>
                <p><%= Html.TextBox("Search", null, new { placeholder = "Postnummer eller ort" })%><input type="submit" value="Sök" /></p>
			</fieldset>
		</form>
	</section>
</aside>

<h2>Din sökning <%= Model.Search %> gav <%= Model.NrOfResults %> träffar</h2>

<ul>
    <% foreach (var item in ViewData.Model.Shops) { %>
        <li>
            <dl>
                <dd>Id</dd><dt><%= item.Id %></dt>
                <dd>Namn</dd><dt><%= item.Name %></dt>
                <dd>Hemsida</dd><dt><%= item.HomePage %></dt>
                <dd>Telefonnummer</dd><dt><%= item.Telephone %></dt>
                <dd>Karta</dd><dt><%= item.Map %></dt>
                <dd>Email</dd><dt><%= item.Email %></dt>
                <dd>Longitude</dd><dt><%= item.Longitude %></dt>
                <dd>Latitude</dd><dt><%= item.Latitude %></dt>
                <dd>Gatuadress</dd><dt><%= item.StreetAddress %></dt>
            </dl>
        </li>
     <% } %>
</ul>


