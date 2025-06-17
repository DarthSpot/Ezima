import {inject, NgModule, provideBrowserGlobalErrorListeners} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import {ApiModule} from './shared/api/api.module';
import {
  HttpEvent,
  HttpHandler,
  HttpHandlerFn,
  HttpRequest,
  provideHttpClient,
  withInterceptors
} from '@angular/common/http';
import { Dashboard } from './components/dashboard/dashboard';
import { ChildDetails } from './components/child-details/child-details';
import { RewardDetails } from './components/reward-details/reward-details';
import { RewardActivityDetails } from './components/reward-activity-details/reward-activity-details';
import { ChildrenOverview } from './components/children-overview/children-overview';
import { RewardOverview } from './components/reward-overview/reward-overview';
import { UsageOverview } from './components/usage-overview/usage-overview';
import { ActivityOverview } from './components/activity-overview/activity-overview';
import { UserProfileInfo } from './components/user-profile-info/user-profile-info';
import {Observable} from 'rxjs';
import { LoginSuccess } from './components/login-success/login-success';
import { MainShell } from './components/main/main-shell';
import { PageNotFound } from './components/page-not-found/page-not-found';
import {provideRouter} from '@angular/router';
import {TokenService} from './service/token-service';
import { Welcome } from './components/welcome/welcome';
import {FormsModule} from '@angular/forms';
import { MinutesToHoursPipe } from './minutes-to-hours-pipe';

export function apiTokenInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const authToken = inject(TokenService).authToken;
  if (authToken == null) {
    console.log('Request: Not logged in')
  } else {
    console.log('requesting', req);
    req = req.clone({
      setHeaders: {
        'Authorization': `Bearer ${authToken}`
      }
    })
  }
  return next(req);
}

@NgModule({
  declarations: [
    App,
    Dashboard,
    ChildDetails,
    RewardDetails,
    RewardActivityDetails,
    ChildrenOverview,
    RewardOverview,
    UsageOverview,
    ActivityOverview,
    UserProfileInfo,
    LoginSuccess,
    MainShell,
    PageNotFound,
    Welcome,
    MinutesToHoursPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule.forRoot({rootUrl: 'http://localhost:5044'}),
    FormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([apiTokenInterceptor]))
  ],
  bootstrap: [App]
})
export class AppModule { }
