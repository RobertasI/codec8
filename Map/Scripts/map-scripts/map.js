function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(55, 25),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.HYBRID
    }
    map = new google.maps.Map(document.getElementById("map"), mapOptions);
}

getDataFromController();

function addMarker(imei, long, lat) {

    var infowindow = new google.maps.InfoWindow({
        content: String(imei)
    });

    marker = new google.maps.Marker({
        position: new google.maps.LatLng(lat, long),
        map: map
    });
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
}


function getDataFromController() {
    var dataList = [];
    $.ajax({
        url: '/Home/GetDataBaseData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // process the data coming back
            $.each(data, function (index, item) {
   
                addMarker(item.Imei, item.Longitude / 1000000, item.Latitude / 1000000)

                dataList.push({ ime: item.Imei, latitude: item.Latitude / 1000000, longitude: item.Longitude / 1000000 })
            });
            findUniqueImeis(dataList);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function findUniqueImeis(dataList) {

    var imeiList = [dataList[0].ime];
    var dataListLenght = 0;

    for (let item of dataList) {
        dataListLenght = dataListLenght + 1;
    }

    console.log(dataListLenght);
    
    for (var i = 1; i < dataListLenght; i++) {
        var j = 0;
        var unique = true;
        while (j < imeiList.length) {
            if (dataList[i].ime == imeiList[j]) {
                unique = false;
                j++
            }
            else { j++; }
        }
        if (unique) {
            imeiList.push(dataList[i].ime);
        }
    }

    for (let item of imeiList) {

        console.log(item);
    }
 
    getDataForLines(imeiList, dataList);
}

function getDataForLines(imeiList, dataList) {

    for (var i = 0; i < imeiList.length; i++) {
        var data = [];
        for (let item of dataList) {

            if (imeiList[i] === item.ime) {
                data.push({lat:item.latitude, lng:item.longitude});
            }
        }
        console.log(data);
        drawLines(data);
    }
}

function drawLines(data) {

    var mapPath = new google.maps.Polyline({
        path: data,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 1
    });
    mapPath.setMap(map);

}
