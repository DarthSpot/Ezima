import {inject, Injectable} from '@angular/core';
import {CookieService} from 'ngx-cookie-service';

const ezima_login_token: string = 'ezima_login_token';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  set authToken(value: string) {
    localStorage.setItem(ezima_login_token, value);
  }

  get authToken(): string | null {
    return localStorage.getItem(ezima_login_token);
  }

  clearToken() {
    localStorage.removeItem(ezima_login_token);
  }
}
