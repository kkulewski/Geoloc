import { Component, Inject } from "@angular/core";
import { Http } from "@angular/http";


@Component({
    selector: "location",
    templateUrl: "./location.component.html"
})
export class LocationComponent {

    location: Location;
    locationAvailable: boolean;
    result: string = "not run";

    constructor(private http: Http, @Inject("BASE_URL") private baseUrl: string) {}

    sendLocation() {
        this.http.post(this.baseUrl + "api/Location/Send", this.location).subscribe(error => console.error(error));
    }

    locate() {
        if (!navigator.geolocation) {
            this.locationAvailable = false;
            this.result = "navigator not available";
            return;
        }

        this.result = "getting...";

        navigator.geolocation.getCurrentPosition(
            pos => this.onLocateSuccess(pos),
            error => this.onLocateFailure
        );
    }

    private onLocateSuccess(position: Position) {
        this.locationAvailable = true;
        this.result = "success";
        this.location = new Location();
        this.location.latitude = position.coords.latitude;
        this.location.longitude = position.coords.longitude;
        this.location.timestamp = Date.now();
        let userName = localStorage.getItem("user_name");
        if (userName != null) {
            this.location.userName = userName;
        }
        this.sendLocation();
    }

    private onLocateFailure() {
        this.locationAvailable = false;
        this.result = "navigator not available";
    }
}

export class Location {
    longitude: number;
    latitude: number;
    timestamp: number;
    userName: string;
}