/***********************************************************************************/
function detectUserLocation() {
    //alert("detectando posicion");
    var coords = {
        longitude: -99.150485,
        latitude: 19.424018
    };
    var position = {
        coords: coords
    };
    if (navigator.geolocation) {
        var timeoutVal = 10 * 1000 * 1000;
        navigator.geolocation.getCurrentPosition(
                mapToPosition,
                alertError,
                { enableHighAccuracy: true, timeout: timeoutVal, maximumAge: 0 }
        );
    }
    else {
        alert("La geolocalización no está soportada en este navegador" + "\nFavor de Buscar su Direccion");
        mapToPosition(position);
    }

    function alertError(error) {
        var errors = {
            1: 'Permiso denegado',
            2: 'Posicion no disponible',
            3: 'Tiempo excedido'
        };
        alert("Error: " + errors[error.code] + "\nFavor de Buscar su Direccion");
        mapToPosition(position);
    }
}

/***********************************************************************************/
function mapToPosition(position) {
    //console.log('mapTo: ' + position.coords.longitude);

    var lon = position.coords.longitude;
    var lat = position.coords.latitude;

    //alert ("lat: " + lat);
    map.setCenter(new google.maps.LatLng(lat, lon));
}