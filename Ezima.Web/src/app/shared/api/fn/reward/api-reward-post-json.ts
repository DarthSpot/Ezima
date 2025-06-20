/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { Reward } from '../../models/reward';
import { RewardRequest } from '../../models/reward-request';

export interface ApiRewardPost$Json$Params {
      body?: RewardRequest
}

export function apiRewardPost$Json(http: HttpClient, rootUrl: string, params?: ApiRewardPost$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Reward>> {
  const rb = new RequestBuilder(rootUrl, apiRewardPost$Json.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Reward>;
    })
  );
}

apiRewardPost$Json.PATH = '/api/reward';
