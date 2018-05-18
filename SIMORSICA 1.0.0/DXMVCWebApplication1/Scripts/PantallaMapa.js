

function goFullScreen() {
    var center = map.getCenter();
    mapDiv.style.position = "fixed";
    mapDiv.style.width = "100%";
    mapDiv.style.height = "100%";
    mapDiv.style.top = "0";
    mapDiv.style.left = "0";
    mapDiv.style.zIndex = "100000";
    document.body.style.overflow = "hidden";
    //$(controlDiv).find("div div").html(exitFull);
    fullScreen = true;
    google.maps.event.trigger(map, "resize");
    map.setCenter(center);
    // this works around street view causing the map to disappear, which is caused by Google Maps setting the 
    // css position back to relative. There is no event triggered when Street View is shown hence the use of setInterval
    interval = setInterval(function () {
        if (mapDiv.style.position !== "fixed") {
            mapDiv.style.position = "fixed";
            google.maps.event.trigger(map, "resize");
        }
    }, 100);
}

function exitFullScreen() {
    var center = map.getCenter();
    if (originalPos === "") {
        mapDiv.style.position = "relative";
    } else {
        mapDiv.style.position = originalPos;
    }
    mapDiv.style.width = originalWidth;
    mapDiv.style.height = originalHeight;
    mapDiv.style.top = originalTop;
    mapDiv.style.left = originalLeft;
    mapDiv.style.zIndex = originalZIndex;
    document.body.style.overflow = originalOverflow;
    //$(controlDiv).find("div div").html(enterFull);
    fullScreen = false;
    google.maps.event.trigger(map, "resize");
    map.setCenter(center);
    clearInterval(interval);
}