import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { IRegisterModel } from '../models/register.model';

@Component({
    selector: 'register',
    templateUrl: './register.component.html'
})
export class RegisterComponent {
    isRequesting: boolean = false;
    resultMessage = "";

    constructor(private router: Router, private http: Http, @Inject("BASE_URL") private baseUrl: string) {}

    registerUser({ value, valid }: { value: IRegisterModel, valid: boolean }) {
        this.isRequesting = true;
        if (valid) {
            this.register(value.email, value.password, value.firstName, value.lastName)
                .subscribe(
                    (response: Response) => {
                        this.resultMessage = response.text();
                        this.isRequesting = false;
                    },
                    error => {
                        this.resultMessage = error.text();
                        this.isRequesting = false;
                    });
        }
    }

    register(email: string, password: string, firstName: string, lastName: string) {
        let body = JSON.stringify({ email, password, firstName, lastName });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + "api/account/register", body, options);
    }
}
