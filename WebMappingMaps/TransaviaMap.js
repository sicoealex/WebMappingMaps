var map;
function initMap() {
    var abrud = { lat: 46.37557, lng: 23.101916 };
    map = new google.maps.Map(document.getElementById('map'), {
        center: abrud,
        zoom: 13
    });
}