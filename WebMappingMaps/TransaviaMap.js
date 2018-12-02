var map;
function initMap() {
    var abrud = { lat: 46.6333, lng: 23.6667};
    map = new google.maps.Map(document.getElementById('map'), {
        center: abrud,
        zoom: 12
    });
}

var getJSON = function (url, callback) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            console.log('responseText:' + xmlhttp.responseText);
            try {
                var data = JSON.parse(xmlhttp.responseText);
            } catch (err) {
                console.log(err.message + " in " + xmlhttp.responseText);
                return;
            }
            callback(data);
        }
    };

    xmlhttp.open("GET", url, true);
    xmlhttp.send();
};

function infoCallback(infowindow) {
    return function () {
        infowindow.open(map);
    };
}

function polygonCenter(poly) {
    var lowx,
        highx,
        lowy,
        highy,
        lats = [],
        lngs = [],
        vertices = poly.getPath();

    for (var i = 0; i < vertices.length; i++) {
        lngs.push(vertices.getAt(i).lng());
        lats.push(vertices.getAt(i).lat());
    }

    lats.sort();
    lngs.sort();
    lowx = lats[0];
    highx = lats[vertices.length - 1];
    lowy = lngs[0];
    highy = lngs[vertices.length - 1];
    center_x = lowx + ((highx - lowx) / 2);
    center_y = lowy + ((highy - lowy) / 2);
    return (new google.maps.LatLng(center_x, center_y));
}

function addPoly(polyPath, polyInfo, line_color, fill_colour) {
    var transvPolyg = new google.maps.Polygon({
        paths: polyPath,
        strokeColor: line_color,
        strokeOpacity: 0.9,
        strokeWeight: 1,
        fillColor: fill_colour,
        fillOpacity: 0.35
    });

    var infowindow = new google.maps.InfoWindow({
        content: polyInfo,
        position: polygonCenter(transvPolyg)
    });
    google.maps.event.addListener(transvPolyg, 'click', infoCallback(infowindow));

    transvPolyg.setMap(map);
}


function initializeMap() {
    var arr = [];
    getJSON("http://localhost:57843/api/GetPolygonLocation/getTransaviaSpatialLocation", function (data) {

        for (var i in data) {
            var polyCoordinates = data[i].coordinates;
            arr.push({
                description: data[i].metadata.tipCultura,
                colorMap: data[i].metadata.collorMap
            });
            var polyColor = data[i].metadata.collorMap;
            var contentString = '<b>Tip Cultura: ' + data[i].description + '</b><br>' +
                'Suprafata: ' + data[i].metadata.suprafata +
                '<br> <br>';
            addPoly(polyCoordinates, contentString, polyColor, polyColor);
        }

    });


    getJSON("http://localhost:57843/api/GetPolygonLocation/getLegendModels", function (distArr) {

        var legend = document.getElementById('legend');
        for (var key in distArr) {
            var type = distArr[key];
            var name = type.tipCultura;
            var color = type.collorMap;
            //var icon = type.icon;
            var ul = document.getElementById('legss');
            //div.innerHTML = '<span style="height:32px;width:32px;background:"' + type.collorMap + ';">' + name + '</span>';
            ul.innerHTML += '<li><span style=background:' + color + ';"></span>' + name + '</li>';
            //legend.appendChild(div);
        }

        map.controls[google.maps.ControlPosition.RIGHT_TOP].push(legend);
    });

}

/*function loadMap() {
    var arr = [];
    getJSON("http://localhost:57843/api/GetPolygonLocation/getTransaviaSpatialLocation", function (data) {

        for (var i in data) {
            var flightPlanCoordinates = data[i].coordinates;
            //if (arr.description.include(data[i].description))
            arr.push({
                description: data[i].metadata.tipCultura,
                colorMap: data[i].metadata.collorMap
            });
            //var distArr = [...new Set(arr.map(x => x.description))];

            var color = data[i].metadata.collorMap;
            var bermudaTriangle = new google.maps.Polygon({
                paths: flightPlanCoordinates,
                strokeColor: color,
                strokeOpacity: 0.9,
                strokeWeight: 1,
                fillColor: color,
                fillOpacity: 0.35
            });
            bermudaTriangle.setMap(map);

            bermudaTriangle.addListener('click', function(e){
                showArrays.call(this, e, data[i]);
            });
            infoWindow = new google.maps.InfoWindow;
        };
    });

    function showArrays(event, param) {
        // Since this polygon has only one path, we can call getPath() to return the
        // MVCArray of LatLngs.
        //var vertices = this.getPath();

        var contentString = '<b>Tip Cultura: ' + param.description + '</b><br>' +
            'Suprafata: ' + param.metadata.suprafata +
            '<br> <br>';
        infoWindow.setContent(contentString);
        infoWindow.setPosition(event.latLng);

        infoWindow.open(map);
    }

    getJSON("http://localhost:57843/api/GetPolygonLocation/getLegendModels", function (distArr) {

        var legend = document.getElementById('legend');
        for (var key in distArr) {
            var type = distArr[key];
            var name = type.tipCultura;
            var color = type.collorMap;
            //var icon = type.icon;
            var ul = document.getElementById('legss');
            //div.innerHTML = '<span style="height:32px;width:32px;background:"' + type.collorMap + ';">' + name + '</span>';
            ul.innerHTML += '<li><span style=background:' + color + ';"></span>' + name + '</li>';
            //legend.appendChild(div);
        }

        map.controls[google.maps.ControlPosition.RIGHT_TOP].push(legend);
    });

}*/