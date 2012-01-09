<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.ShopListItem>>" %>

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
        var latlng;

        <% foreach (var item in ViewData.Model) { %>
        latlng = AddElementToMap(map, <%= item.Latitude %>, <%= item.Longitude %>, "<%= item.Description %>", "<%= item.Name %>");
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

<div id="map_canvas" style="width: 800px; height: 600px;"></div>
<a href class="resize">Visa större karta</a>