import {Component, inject, OnInit} from '@angular/core';
import {Observable, Subject} from 'rxjs';
import {TokenService} from '../../service/token-service';

@Component({
  selector: 'app-main',
  standalone: false,
  templateUrl: './main-shell.html',
  styleUrl: './main-shell.css'
})
export class MainShell implements OnInit {
  tokenService = inject(TokenService);
  isLoggedIn = this.tokenService.authToken != null;

  ngOnInit(): void {

  }


}
