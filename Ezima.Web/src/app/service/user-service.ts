import {inject, Injectable} from '@angular/core';
import {LoggingService} from './logging-service';
import {catchError, Observable, of, tap} from 'rxjs';
import {UserInfo} from '../shared/api/models/user-info';
import {AuthApiService} from '../shared/api/services';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private logger = inject(LoggingService);
  private authService = inject(AuthApiService);

  constructor() { }

  getUserInfo() {
    return this.authService.apiAuthGet$Json()
      .pipe(
        tap(_ => this.logger.log('Getting user data')),
        catchError(this.handleError<UserInfo>('getUserInfo'))
      )
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error); // log to console instead

      this.logger.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
