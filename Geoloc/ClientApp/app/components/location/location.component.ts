import { Component } from '@angular/core';

@Component({
    selector: 'location',
    templateUrl: './location.component.html'
})
export class LocationComponent {
    public location = new Location(0, 0);

    public locate() {
        navigator.geolocation.getCurrentPosition((pos) => this.setLocationSuccess(pos), this.setLocationFail, { timeout: 10000 });
    }

    public setLocationSuccess(position: Position) {
        this.location = new Location(position.coords.latitude, position.coords.longitude);
    }

    public setLocationFail() {
        this.location = new Location(-1, -1);
    }
}

export class Location {
    constructor(public latitude: number, public longitude: number) {
        this.latitude = latitude;
        this.longitude = longitude;
    }
}
