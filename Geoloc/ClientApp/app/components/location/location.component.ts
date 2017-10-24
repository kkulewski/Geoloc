import { Component } from '@angular/core';

@Component({
    selector: 'location',
    templateUrl: './location.component.html'
})
export class LocationComponent {
    public location?: Location;
    public locationAvailable: boolean;

    public locate() {
        if (!navigator.geolocation) {
            this.locationAvailable = false;
            return;
        }

        navigator.geolocation.getCurrentPosition(
            (pos) => this.setLocationSuccess(pos),
            this.setLocationFail,
            { timeout: 10000 }
        );
    }

    public setLocationSuccess(position: Position) {
        this.locationAvailable = true;
        this.location = new Location(position.coords.latitude, position.coords.longitude);
    }

    public setLocationFail() {
        this.locationAvailable = false;
        this.location = undefined;
    }
}

export class Location {
    constructor(public latitude: number, public longitude: number) {
        this.latitude = latitude;
        this.longitude = longitude;
    }
}
