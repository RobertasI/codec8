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

var dataList = [];
function getDataFromController() {
    $.ajax({
        url: '/Home/GetDataBaseData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // process the data coming back
            $.each(data, function (index, item) {
   
                addMarker(item.Longitude / 1000000, item.Latitude / 1000000)

                dataList.push({ ime: item.Imei, latitude: item.Latitude / 1000000, longitude: item.Longitude / 1000000 })
            });
            addLines(dataList);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function addLines(dataList) {

    //apsiskaiciuoti visus skirtingus imei
    var imeiList = [dataList[0].ime];
    var dataListLenght = 0;

    for (let item of dataList) {
        dataListLenght = dataListLenght + 1;
    }

    console.log(dataListLenght);
    
    for (var i = 1; i < dataListLenght; i++) {
        console.log("i=", i)
        //for (var j = 0; j < imeiList.lenght + 1; j++) {
        var j = 0;
        while (j < imeiList.length) {
            console.log("j = ", j)

            console.log("imeilistlenght: ", imeiList.length)
            if (dataList[i] !== dataList[j]) {
                imeiList.push(dataList[i]);
                j++;
            }
        }
    }

    
    for (let item of imeiList) {

        console.log(item);
    }
   

    //var mapPath = new google.maps.Polyline({
    //    path: dataList,
    //    geodesic: true,
    //    strokeColor: '#FF0000',
    //    strokeOpacity: 1.0,
    //    strokeWeight: 2
    //});
    //mapPath.setMap(map);
}



/////console.log(item.Longitude / 1000000, item.Latitude / 1000000)
/////console.log(index, item.Imei);