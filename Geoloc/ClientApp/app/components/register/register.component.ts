import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'register',
    templateUrl: './register.component.html'
})
export class RegisterComponent {
    isRequesting: boolean;
    submitted: boolean = false;

    constructor(private router: Router, private http: Http, @Inject("BASE_URL") private baseUrl: string) {}

    registerUser({ value, valid }: { value: IUserRegistration, valid: boolean }) {
        this.submitted = true;
        this.isRequesting = true;
        if (valid) {
            this.register(value.email, value.password, value.firstName, value.lastName)
                .subscribe(
                    result => {
                        if (result) {
                            this.router.navigate(['/home'], { queryParams: { name: value.firstName } });
                        }
                    },
                    errors => console.error(errors));
        }
    }

    register(email: string, password: string, firstName: string, lastName: string) {
        let body = JSON.stringify({ email, password, firstName, lastName });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + "api/account/register", body, options);
    }
}

export interface IUserRegistration {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
}
