import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common'
import {CookieService} from 'ngx-cookie-service';
import {TokenService} from '../../service/token-service';

@Component({
  selector: 'app-login-success',
  standalone: false,
  templateUrl: './login-success.html',
  styleUrl: './login-success.css'
})
export class LoginSuccess implements OnInit {
  public message: string = "Hallo Welt";
  private cookieService = inject(CookieService);
  private tokenService = inject(TokenService);

  constructor(private route: ActivatedRoute,
              private location: Location) {
  }

  ngOnInit(): void {
    this.checkLogin();
  }

  private checkLogin() {
    const token = this.cookieService.get('access_token');
    this.tokenService.authToken = token;
  }
}
