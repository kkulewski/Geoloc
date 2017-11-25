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

    userId() {
        return localStorage.getItem("user_id");
    }

    userName() {
        let name = localStorage.getItem("user_name");
        if (name == null) {
            this.getUserName();
        }
        return localStorage.getItem("user_name");
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
                        let response = result.json() as ILoginResponse;
                        localStorage.setItem("auth_token", response.auth_token);
                        localStorage.setItem("user_id", response.id);
                        this.resultMessage = result.text();
                        this.isRequesting = false;
                    },
                    error => {
                        this.resultMessage = error.text();
                        this.isRequesting = false;
                    });
        }
    }

    getUserName() {
        if (!this.isRequesting) {
            this.isRequesting = true;
            let id = this.userId();
            if (id != null) {
                this.accountService.getUserName(id)
                    .subscribe(
                        result => {
                            localStorage.setItem("user_name", result.json() as string);
                            this.isRequesting = false;
                        },
                        error => {
                            console.error(error);
                            this.isRequesting = false;
                        });
            }
        }
    }

    logoutUser() {
        localStorage.removeItem("auth_token");
        localStorage.removeItem("user_id");
        localStorage.removeItem("user_name");
    }
}

export interface ILoginResponse {
    id: string;
    auth_token: string; 
}
