import { inject, Injectable} from '@angular/core';
import { catchError, Observable, of, tap} from 'rxjs';
import { Child } from '../shared/api/models/child';
import { LoggingService } from './logging-service';
import { ChildCreationRequest } from '../shared/api/models/child-creation-request';
import { ChildApiService } from '../shared/api/services';
import { ChildInfo } from '../shared/api/models/child-info';

@Injectable({
  providedIn: 'root'
})
export class ChildService {
  logger = inject(LoggingService);
  childApiService = inject(ChildApiService);
  constructor() { }

  getChildren(): Observable<ChildInfo[]> {
    return this.childApiService.apiChildInfosGet$Json()
      .pipe(
        tap(_ => this.log('fetching children')),
        catchError(this.handleError<ChildInfo[]>('getChildren', []))
      )
  }

  addChild(child: ChildCreationRequest): Observable<Child> {
    return this.childApiService.apiChildPost$Json({body: child})
      .pipe(
        tap(_ => this.log(`added child with name "${child.name}"`)),
        catchError(this.handleError<Child>('addChild'))
      )
  }

  getChild(id: number): Observable<Child> {
    return this.childApiService.apiChildChildIdGet$Json({childId: id})
      .pipe(
        tap(_ => this.log(`requesting child with id "${id}"`)),
        catchError(this.handleError<Child>('getChild'))
      )
  }

  private log(message: string) {
    this.logger.log('Child Controller: ' + message);
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

      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
