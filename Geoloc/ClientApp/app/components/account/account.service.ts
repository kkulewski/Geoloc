import { Http, Headers, RequestOptions } from '@angular/http';

export class AccountService {

    constructor(private http: Http, private baseUrl: string) {}

    register(email: string, password: string, firstName: string, lastName: string) {
        let body = JSON.stringify({ email, password, firstName, lastName });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + "api/account/register", body, options);
    }

    login(userName: string, password: string) {
        let body = JSON.stringify({ userName, password });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + 'api/account/login', body, options);
    }
}