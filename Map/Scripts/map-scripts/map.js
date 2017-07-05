function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(51.5, 25),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.HYBRID
    }
    map = new google.maps.Map(document.getElementById("map"), mapOptions);
    addMarker();

}

function addMarker() {
    marker = new google.maps.Marker({
        position: new google.maps.LatLng(51.5, 25),
        map: map
    });
}

