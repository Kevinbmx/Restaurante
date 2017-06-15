var MapSelector = (function (document) {
    "use strict";
    var MapSelector = function (id, canvas, latHF, lngHF, markerLabel, editable, clearButton) {
        if (!window.MapSelectors)
            window.MapSelectors = {};

        window.MapSelectors[id.replace("#", "")] = this;
        
        this.id = id;
        this.lat = 0;
        this.latHF = latHF;
        this.lngHF = lngHF;
        this.lng = 0;
        this.map = null;
        this.marker = null;
        this.editable = editable;
        this.markerLabel = markerLabel;
        this.clearButton = clearButton;

        if ($(latHF).val() != '')
            this.lat = parseFloat($(latHF).val());
        if ($(lngHF).val() != '')
            this.lng = parseFloat($(lngHF).val());

        if (this.lat == 0 && this.lng == 0) {
            this.lat = -17.783474;
            this.lng = -63.182153;
            $(this.id).val("");
        } else
            $(this.id).val("0");

        var centerCoordinates = new google.maps.LatLng(this.lat, this.lng);
        var mapOptions = {
            center: centerCoordinates,
            zoom: 10,
            draggableCursor: "default",
            draggingCursor: "hand"
        };
        this.map = new google.maps.Map(document.getElementById(canvas),
            mapOptions);
        if ($(this.id).val() != "") {
            this.marker = new google.maps.Marker({
                position: centerCoordinates,
                map: this.map,
                draggable: editable
            });
        }

        var self = this;
        this.onMarkerMouseUp = function () {
            var newPos = self.marker.getPosition();
            $(self.latHF).val(newPos.lat);
            $(self.lngHF).val(newPos.lng);
            $(self.id).val("0");
            $(self.clearButton).show();
        }

        this.onClickOnMap = function (event) {
            if (self.marker)
                self.marker.setMap(null);

            var newPos = event.latLng;

            self.marker = new google.maps.Marker({
                position: newPos,
                map: self.map,
                draggable: self.editable
            });

            $(self.latHF).val(newPos.lat);
            $(self.lngHF).val(newPos.lng);
            $(self.id).val("0");
            $(self.clearButton).show();
            self.marker.addListener('mouseup', self.onMarkerMouseUp);
            
        }

        this.clear = function () {
            if (self.marker)
                self.marker.setMap(null);
            $(self.latHF).val("0");
            $(self.lngHF).val("0");
            $(self.id).val("");
            $(self.clearButton).hide();
        }

        this.refresh = function () {
            google.maps.event.trigger(self.map, 'resize');
        }

        this.setMarker = function (lat, lng) {
            if (self.marker)
                self.marker.setMap(null);

            self.lat = lat;
            self.lng = lng;
            var newPos = new google.maps.LatLng(self.lat, self.lng);;

            self.marker = new google.maps.Marker({
                position: newPos,
                map: self.map,
                draggable: self.editable
            });

            var bounds = new google.maps.LatLngBounds();
            var infowindow = new google.maps.InfoWindow();
            bounds.extend(self.marker.position);
            self.map.fitBounds(bounds);

            $(self.latHF).val(newPos.lat);
            $(self.lngHF).val(newPos.lng);
            $(self.id).val("0");
            $(self.clearButton).show();
            self.marker.addListener('mouseup', self.onMarkerMouseUp);
            self.map.setCenter(self.marker.position);
            self.map.setZoom(15);
        }

        if (this.editable) {
            if (this.marker)
                this.marker.addListener('mouseup', this.onMarkerMouseUp);
            else
                $(this.clearButton).hide();
            google.maps.event.addListener(this.map, "click", this.onClickOnMap);
            $(this.clearButton).click(function () {
                self.clear();
                return false;
            });

        } else {
            $(this.clearButton).hide();
        }
    }

   
    return MapSelector;
})(document);

MapSelector.getMapSelector = function(id) {
    if (!window.MapSelectors)
        return null;
    return window.MapSelectors[id.replace("#", "")];
}