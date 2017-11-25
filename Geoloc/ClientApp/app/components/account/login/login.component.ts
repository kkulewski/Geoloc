import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Response } from '@angular/http';
import { ILoginModel } from '../models/login.model';
import { AccountService } from '../account.service';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    isRequesting: boolean = false;
    resultMessage = "";
    accountService: AccountService;

    authToken() {
        return localStorage.getItem("auth_token");
    }

    constructor(private router: Router, private http: Http, @Inject("BASE_URL") private baseUrl: string) {
        this.accountService = new AccountService(this.http, this.baseUrl);
    }

    loginUser({ value, valid }: { value: ILoginModel, valid: boolean }) {
        this.isRequesting = true;
        if (valid) {
            this.accountService.login(value.email, value.password)
                .subscribe(
                    result => {
                        localStorage.setItem("auth_token", (result.json() as ILoginResponse).auth_token);
                        this.resultMessage = result.text();
                        this.isRequesting = false;
                    },
                    error => {
                        this.resultMessage = error.text();
                        this.isRequesting = false;
                    });
        }
    }
}

export interface ILoginResponse {
    id: string;
    auth_token: string; 
}
