/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { RewardUsage } from '../../models/reward-usage';
import { RewardUsageRequest } from '../../models/reward-usage-request';

export interface ApiUsagePost$Json$Params {
      body?: RewardUsageRequest
}

export function apiUsagePost$Json(http: HttpClient, rootUrl: string, params?: ApiUsagePost$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<RewardUsage>> {
  const rb = new RequestBuilder(rootUrl, apiUsagePost$Json.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<RewardUsage>;
    })
  );
}

apiUsagePost$Json.PATH = '/api/usage';
