import { Component } from '@angular/core';

@Component({
    selector: 'location',
    templateUrl: './location.component.html'
})
export class LocationComponent {
    public latitude = 0;
    public longitude = 0;

    public locate() {
        navigator.geolocation.getCurrentPosition((pos) => this.setLocationSuccess(pos), this.setLocationFail, { timeout: 10000 });
    }

    public setLocationSuccess(position: Position) {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
    }

    public setLocationFail() {
        this.latitude = -1;
        this.longitude = -1;
    }
}
