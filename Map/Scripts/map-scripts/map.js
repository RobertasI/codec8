function initMarker() {
    var uluru = { lat: -25.363, lng: 131.044 };
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}