//globales
var clickListener;
var gDireccion;
var btnTerminar = "<br/><input type='button' value='Terminar' onclick='terminar()' id='idTerminar' title='Terminar' />";
var btnCancelar = "<input type='button' value='Cancelar' onclick='cancelar()' id='idCancelar' title='Cancelar' />";
var btnRegresar = "<br/><input type='button' value='Regresar' onclick='exitFullScreen()' id='idRegresar' title='Regresar' />";
var gPosicionMarker //posicion actual del Marker por si se cancela regresarlo a su posicion original
var markers = []; //arreglo de marcadores agregados al mapa durante la edicion

var glatlng = [];
var pointCount = 0;
var nomPoint = [];
var idPoint = [];
var map;
var geocoder;
//var markers = [];
var markersPoints = [];
var resultGeocode;
var selectedMarker = 0; //marcador del Productor Seleccionado
var gdatos = [];



//variables del mapa
var interval;
var mapDiv;
var divStyle;
var originalPos;
var originalWidth;
var originalHeight;
var originalTop;
var inalLeft;
var originalZIndex;
var bodyStyle;
var originalOverflow

/***********************************************************************************/
function SelectionChanged(s, e) {
    //alert("SelectionChanged");
    //s.GetSelectedFieldValues("Pk_idProductor", OnGetValues);
    var existe = false;
    var row;
    var lat;
    var lng;
    var key = s.GetRowKey(e.visibleIndex);
    if (key == null) { //registro borrado
        pointCount = pointCount - 1;
        idPoint.length = 0;
        nomPoint.length = 0;
        glatlng.length = 0;
        markersPoints.length = 0;
        existe = true;
        for (i = 0; i < pointCount; i++) {
            row = s.GetRow(i);
            idPoint[i] = s.GetRowKey(i);
            nomPoint[i] = row.closest('tr').childNodes[2].innerText;;
            lat = row.closest('tr').childNodes[12].innerText;
            lng = row.closest('tr').childNodes[13].innerText;
            glatlng[i] = { lat: lat, lng: lng };;
        }
        asignarMarkers(); //asigna los markersPoints
        return;
    }

    for (i = 0; i < pointCount; i++) {
        if (idPoint[i] == parseInt(key)) {
            if (e.isSelected) { //Mostrar
                markersPoints[i].setMap(map);
                selectedMarker = i;
            } else { //ocultar
                markersPoints[i].setMap(null);
            }
            existe = true;
        }
    }
    if (!existe) { //marker nuevo
        idPoint[pointCount - 1] = parseInt(key);
        markersPoints[pointCount - 1].setMap(map);
        selectedMarker = pointCount - 1;
    }

}

/***********************************************************************************/
function initMap() {
    //alert("initMap");

    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 21.118314, lng: -101.683038 }, //Centro de Leon
        zoom: 11,
        zoomControl: true,
        zoomControlOptions: {
            position: google.maps.ControlPosition.LEFT_CENTER
        },
        scaleControl: true,
        streetViewControl: true,
        streetViewControlOptions: {
            position: google.maps.ControlPosition.LEFT_TOP
        }
    });

    //variables del mapa
    mapDiv = map.getDiv();
    divStyle = mapDiv.style;
    if (mapDiv.runtimeStyle) {
        divStyle = mapDiv.runtimeStyle;
    }
    originalPos = divStyle.position;
    originalWidth = divStyle.width;
    originalHeight = divStyle.height;
    // ie8 hack
    if (originalWidth === "") {
        originalWidth = mapDiv.style.width;
    }
    if (originalHeight === "") {
        originalHeight = mapDiv.style.height;
    }
    originalTop = divStyle.top;
    originalLeft = divStyle.left;
    originalZIndex = divStyle.zIndex;
    bodyStyle = document.body.style;
    if (document.body.runtimeStyle) {
        bodyStyle = document.body.runtimeStyle;
    }
    originalOverflow = bodyStyle.overflow;

    asignarMarkers();
}


/***********************************************************************************/
/*funcion para calcular y crear todos los markers*/
function asignarMarkers() {
    var marker;
    var info = ""
    for (i = 0; i < pointCount; i++) {
        info = crearInfo(i);

        marker = placeMarker(glatlng[i], false, info);
        markersPoints.push(marker);
        markersPoints[i].setMap(null);
    }
}




/***********************************************************************************/
function getEstadoNombreSpatial(Campo,lat, lng) {
    $.ajax({
        type: "GET",
        url: "/getData/getEstadoNombreSpatial",
        data: { lng: lng, lat: lat },
        success: function (data) {
            Campo.SetText(data);
            //return data;
        }
    });
}

/***********************************************************************************/
function getMunicipioSpatial(Campo1, Campo2, lat, lng) {

    $.ajax({
        type: "GET",
        url: "/getData/getMunicipioSpatial",
        data: { lng: lng, lat: lat },
        async: false,
        success: function (data) {
            //return data;
            var arrayDatos = data.split(".");
            Campo1.SetValue(parseInt(arrayDatos[0]));
            document.getElementById(Campo2).value = arrayDatos[1];
            //Campo.SetText(arrayDatos[1]);

        }
    });
}






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
function enviaCoords(Coords) {
    //var coords = "SRID=4326;POINT (" + Coords.lat + " " + Coords.lng + ")";
    $('#Latitud_I').html(Coords.lat);
    $('#Longitud_I').html(Coords.lng);
}


/***********************************************************************************/
//$.when(esperarJavaScript()).done(function (a1) {
//    alert("entrando a UpdateEdit");
//    ProductorGridView.UpdateEdit();
//});

//$(document).ajaxStop(function () {
//    alert("ajaxStop");
//    if (Latitud.GetValue() != null && Longitud.GetValue() != null) {
//        //ProductorGridView.UpdateEdit();
//    }
    
//});

/***********************************************************************************/
function geocodeAddress(address, Regresar) {
    var infoText;
    //for (var i = 0; i < markers.length; i++) {
    //    markers[i].setMap(null);
    //}
    //markers = [];

    //var address = "Nisperos 214 las Huertas Leon Guanajuato Mexico";
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            var Coords = { lat: results[0].geometry.location.lat(), lng: results[0].geometry.location.lng() };
            infoText = results[0].formatted_address;
            if (Regresar) {
                infoText = infoText + btnRegresar;
            }
            //enviaCoords(Coords);
            Latitud.SetValue(Coords.lat);
            Longitud.SetValue(Coords.lng);
            //marker = placeMarker(Coords, false, infoText);
            //markers.push(marker);

            markers[0] = placeMarker(Coords, false, infoText);

        } else {
            alert('La geocodificación no pudo ser completada: ' + status);
        }

    });
    //var valor = esperarJavaScript();
}

/***********************************************************************************/
//function esperarJavaScript() {
//    $.ajax({
//        type: "GET",
//        url: "/getData/esperarJavaScript",
//        async: false,
//        success: function (data) {
//            return data;
//        }
//    });
//}

/***********************************************************************************/
function reverseGeocodeByCoords(arrCoords, Regresar) {
    var infoText;
    var latlng = { lat: parseFloat(arrCoords[0].replace(',', '.')), lng: parseFloat(arrCoords[1].replace(',', '.')) };
    geocoder.geocode({ 'location': latlng, 'language': 'es' }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                infoText = results[0].formatted_address;
                if (Regresar) {
                    infoText = infoText + btnRegresar;
                }
                marker = placeMarker(latlng, true, infoText);
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

/***********************************************************************************/
function actualizarMarker(nombrePunto, s, e) {
    var marker;
    //determinar si es nuevo o actualizacion
    if (btnUpdate.contentDiv.innerText.search("Guardar") != -1) { //nuevo
        var infowindow = new google.maps.InfoWindow;
        var posicion = markers[0].getPosition();

        markers[0].setMap(null);
        markers.length = 0;

        nomPoint[pointCount] = $(nombrePunto).val();
        info = crearInfo(pointCount);
        marker = placeMarker(posicion, false, info);
        markersPoints.push(marker);
        marker.setMap(null);
        markersPoints[pointCount].setMap(null);

        idPoint[pointCount] = 0;
        //var lat = parseFloat(21);
        //var lng = parseFloat(-101);
        //latlng[pointCount] = { lat: lat, lng: lng };
        pointCount = pointCount + 1;
        return;

    }
    else { //actualizacion
        //var infowindow = new google.maps.InfoWindow;
        var posicion = markers[0].getPosition();

        //gPosicionMarker = markersPoints[selectedMarker].getPosition();
        markersPoints[selectedMarker].setPosition(posicion);
        //markersPoints[0].setPosition(posicion);
        ocultarMarker();
    }
}

/******************************************************************************************/
function ocultarMarker() {
    if (markers.length >= 1) {
        markers[0].setMap(null);
        //    for (var i = 0; i < markers.length; i++) {
        //        markers[i].setMap(null);
        //    }
        markers = null;
        markers = [];
    }
}


function terminar() {
    //alert('hola');
    exitFullScreen();
    google.maps.event.removeListener(clickListener);
    actualizaDireccion(gDireccion);
}

function cancelar() {
    alert('Se cancela la captura en mapa');
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

