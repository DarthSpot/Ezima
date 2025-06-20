/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';


export interface ApiUsageChildChildIdTotalGet$Plain$Params {
  childId: number;
}

export function apiUsageChildChildIdTotalGet$Plain(http: HttpClient, rootUrl: string, params: ApiUsageChildChildIdTotalGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<number>> {
  const rb = new RequestBuilder(rootUrl, apiUsageChildChildIdTotalGet$Plain.PATH, 'get');
  if (params) {
    rb.path('childId', params.childId, {});
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return (r as HttpResponse<any>).clone({ body: parseFloat(String((r as HttpResponse<any>).body)) }) as StrictHttpResponse<number>;
    })
  );
}

apiUsageChildChildIdTotalGet$Plain.PATH = '/api/usage/child/{childId}/total';
