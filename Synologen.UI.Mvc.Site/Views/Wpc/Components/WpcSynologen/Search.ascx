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

<article>
    <figure class="map">
        <div id="map_canvas" style="width: 800px; height: 600px"></div>
    </figure>
    <a href class="resize">Visa större karta</a>
</article>

<div id="search-results">
    <h1>Sökresultat</h1>

    <h2>Din sökning <%= Model.Search %> gav <%= Model.NrOfResults %> träffar</h2>

    <% foreach (var item in ViewData.Model.Shops) { %>
        <article class="store-information">
            <h2><%= item.Name %></h2>
            <p class="tags">Vi erbjuder: <em>Ögonapoteket</em>, <em>Ögonhälsoundersökning</em></p>
            <p>Nullam sit amet adipiscing nisi. Duis viverra nisi non lorem adipiscing consequat. Maecenas sodales placerat lacinia.</p>
            <p>Tfn: <%= item.Telephone %><br />
            Adress: <%= item.StreetAddress %><br />
            E-post: <a href="<%= item.Email %>"><%= item.Email %></a>
            </p>
        </article>
    <% } %>
</div>