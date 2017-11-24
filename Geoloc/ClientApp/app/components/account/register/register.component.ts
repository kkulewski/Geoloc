import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { IRegisterModel } from '../models/register.model';
import { AccountService } from '../account.service';

@Component({
    selector: 'register',
    templateUrl: './register.component.html'
})
export class RegisterComponent {
    isRequesting: boolean = false;
    resultMessage = "";
    accountService: AccountService;

    constructor(private router: Router, private http: Http, @Inject("BASE_URL") private baseUrl: string) { 
      this.accountService = new AccountService(this.http, this.baseUrl)
  }

    registerUser({ value, valid }: { value: IRegisterModel, valid: boolean }) {
        this.isRequesting = true;
        if (valid) {
            this.accountService.register(value.email, value.password, value.firstName, value.lastName)
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
}
