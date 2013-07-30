mapGoogle = function () {
};

mapGoogle.prototype.init = function (options) {

    var thisObj = this;

    this.mapOptions = {
        center: new google.maps.LatLng(options.latitude, options.longitude),
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        mapTypeControl: false,
        streetViewControl: false
    };


};

mapGoogle.prototype.createMap = function (elementId, latitude, longitude, locations, tabSelected) {

    var thisObj = this;
    this.map = new google.maps.Map(document.getElementById(elementId), this.mapOptions);

    var location = new google.maps.LatLng(latitude, longitude);

    var markerImage = new google.maps.MarkerImage("/Content/themes/base/images/common/mapSchool.png");
    var markerOptions = {
        title: (tabSelected == "Colegios") ? 'Colegio' : 'Escuela Infantil',
        clickable: true,
        position: location,
        map: this.map,
        icon: markerImage,
        zIndex: 1000
    };

    var bounds = new google.maps.LatLngBounds();
    bounds.extend(location);
    //this.map.fitBounds(bounds);
    this.map.setCenter(location);

    var marker = new google.maps.Marker(markerOptions);

    if (locations != null) {
        $.each(locations, function (index, value) {
            console.log(value);
            var iconUrl = "";
            if (value.TypeSchool === "P\u00FAblico") {
                iconUrl = "/Content/themes/base/icons/pin-blue.png";
            } else if (value.TypeSchool === "Privado") {
                iconUrl = "/Content/themes/base/icons/pin-green.png";
            } else if (value.TypeSchool === "Concertado") {
                iconUrl = "/Content/themes/base/icons/pin-red.png";
            }
            console.log(iconUrl);
            var coordenates = new google.maps.LatLng(value.Latitude, value.Longitude);
            var markerOptions = {
                clickable: true,
                position: coordenates,
                map: thisObj.map,
                icon: window.location.origin + iconUrl
            };
            if (index < 5) {
                bounds.extend(coordenates);
                thisObj.map.fitBounds(bounds);
            }
            var marker = new google.maps.Marker(markerOptions);
            google.maps.event.addListener(marker, 'click', function () {
                var urlDetalles = (tabSelected == "Colegios") ? "/School/View?schoolId=" + value.Id : "/Nursery/View?nurseryId=" + value.Id;
                var urlText = (tabSelected == "Colegios") ? "Ver detalles Colegio" : "Ver detalles Escuela Infantil";
                var infowindow = new google.maps.InfoWindow({
                    content: '<ul style="list-style:none; font-size:12px; padding-left:0px"><li>' + value.Name + '</li><li>' + value.Address + '</li><li>' + value.TypeSchool + '</li><li><a href=' + urlDetalles + '>' + urlText + '</a></li></ul>',
                    maxWidth: 320
                });
                infowindow.open(thisObj.map, marker);
            });
        });
    }

};



mapgoogle = new mapGoogle();