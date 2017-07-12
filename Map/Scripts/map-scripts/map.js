function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(55, 25),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.HYBRID
    }
    map = new google.maps.Map(document.getElementById("map"), mapOptions);
}

getDataFromController();

function addMarker(long, lat) {
    marker = new google.maps.Marker({
        position: new google.maps.LatLng(lat, long),
        map: map
    });
}

function getDataFromController() {
    $.ajax({
        url: '/Home/GetDataBaseData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // process the data coming back
            $.each(data, function (index, item) {
                console.log(index, item.Imei);
                addMarker(item.Longitude / 1000000, item.Latitude / 1000000)
                console.log(item.Longitude / 1000000, item.Latitude / 1000000)
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function addLines(imei, lat, long) {
    var data = [{imei: 0, latitude:0, longitude:0} ];
    

}




