//globales
var clickListener;
var gDireccion;
var btnTerminar = "<br/><input type='button' value='Terminar' onclick='terminar()' id='idTerminar' title='Terminar' />";
var btnCancelar = "<input type='button' value='Cancelar' onclick='cancelar()' id='idCancelar' title='Cancelar' />";


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

    //alert("lat mapToPosition: " + lat);
    map.setCenter(new google.maps.LatLng(lat, lon));
}

/***********************************************************************************/
function geocodeAddress(address) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];

    //var address = "Nisperos 214 las Huertas Leon Guanajuato Mexico";
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            var Coords = { lat: results[0].geometry.location.lat(), lng: results[0].geometry.location.lng() };
            marker = placeMarker(Coords, false, results[0].formatted_address);
            markers.push(marker);
            enviaCoords(Coords);

            //moverMarker();

        } else {
            alert('La geocodificación no pudo ser completada: ' + status);
        }

    });
}

/***********************************************************************************/
function reverseGeocodeByCoords(arrCoords) {
    var latlng = { lat: parseFloat(arrCoords[0].replace(',', '.')), lng: parseFloat(arrCoords[1].replace(',', '.')) };
    geocoder.geocode({ 'location': latlng, 'language': 'es' }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                //gDireccion = results[0];
                marker = placeMarker(latlng, true, results[0].formatted_address);
                markers.push(marker);
                actualizaDireccion(results[0]);
                //alert(results[0].formatted_address);
            } else {
                window.alert('No hay resultados. Pruebe otra vez');
            }
        } else {
            window.alert('Falla de Geocodificacion debido a: ' + status);
        }
    });
}


/***********************************************************************************/
function reverseGeocode() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = null;
    markers = [];  

    map.setOptions({ draggableCursor: 'crosshair' });

    clickListener = google.maps.event.addListener(map, 'click', function (event) {
    var latlng = { lat: event.latLng.lat(), lng: event.latLng.lng() };

    geocoder.geocode({ 'location': latlng, 'language': 'es' }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                //map.setZoom(14);
                alert("Puede recorrer el punto insertado para una mejor precisión");
                gDireccion = results[0];
                var info = results[0].formatted_address + btnTerminar + btnCancelar;
                marker = placeMarker(latlng, true, info);
                markers.push(marker);
                
            } else {
                window.alert('No hay resultados. Pruebe otra vez');
            }
        } else {
            window.alert('Falla de Geocodificacion debido a: ' + status);
        }
    });

    map.setOptions({ draggableCursor: '' });

    });
}

function moverMarker() {
    var infowindow = new google.maps.InfoWindow;
    var posicion = markers[0].getPosition();

    //gPosicionMarker = markersProductores[selectedMarker].getPosition();
    markersProductores[selectedMarker].setPosition(posicion);
    eliminarMarker();

}

function eliminarMarker() {
    markers[0].setMap(null);
    //    for (var i = 0; i < markers.length; i++) {
    //        markers[i].setMap(null);
    //    }
    markers = null;
    markers = [];

}


function terminar() {
    //alert('hola');
    //gCancela = false;
    exitFullScreen();
    google.maps.event.removeListener(clickListener);
    actualizaDireccion(gDireccion);

    //llenaDireccion(gDireccion, clickListener);
}

function cancelar() {
    alert('Se cancela la captura en mapa');
    //gCancela = true;
    exitFullScreen();
    google.maps.event.removeListener(clickListener);
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = null;
    markers = [];
}

/***********************************************************************************/
function placeMarker(latLng, desplazable, info) {
    var marker;
    var infowindow = new google.maps.InfoWindow;
    marker = new google.maps.Marker({
        map: map,
        position: latLng,
        draggable: desplazable
    });
    if (desplazable) {
        // evento dragend
        google.maps.event.addListener(marker, "dragend", function () {
            latlng = { lat: marker.getPosition().lat(), lng: marker.getPosition().lng() };
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[0]) { //muestra nueva direccion despues del drag
                        gDireccion = results[0];
                        var info = results[0].formatted_address + btnTerminar + btnCancelar;
                        infowindow.setContent(info);
                        //actualiza el texto de la direccion
                        //llenaDireccion(results[0]);
                    }
                }
            });
        });
    }
    google.maps.event.addListener(marker, "click", function () {
        infowindow.open(map, marker);
    });
    infowindow.setContent(info);
    infowindow.open(map, marker);

    return marker;
}


/***********************************************************************************/
function agregarProductor(idProductor) {
    alert("agregar productor ID = " + idProductor);
}

/***********************************************************************************/
function editarProductor(idProductor) {
    alert("editar productor ID = " + idProductor);
}

/***********************************************************************************/
function eliminarProductor(idProductor) {
    alert("eliminar productor ID = " + idProductor);
}