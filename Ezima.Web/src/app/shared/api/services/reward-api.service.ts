/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { apiRewardChildChildIdGet$Json } from '../fn/reward/api-reward-child-child-id-get-json';
import { ApiRewardChildChildIdGet$Json$Params } from '../fn/reward/api-reward-child-child-id-get-json';
import { apiRewardChildChildIdGet$Plain } from '../fn/reward/api-reward-child-child-id-get-plain';
import { ApiRewardChildChildIdGet$Plain$Params } from '../fn/reward/api-reward-child-child-id-get-plain';
import { apiRewardChildChildIdTotalGet$Json } from '../fn/reward/api-reward-child-child-id-total-get-json';
import { ApiRewardChildChildIdTotalGet$Json$Params } from '../fn/reward/api-reward-child-child-id-total-get-json';
import { apiRewardChildChildIdTotalGet$Plain } from '../fn/reward/api-reward-child-child-id-total-get-plain';
import { ApiRewardChildChildIdTotalGet$Plain$Params } from '../fn/reward/api-reward-child-child-id-total-get-plain';
import { apiRewardGet$Json } from '../fn/reward/api-reward-get-json';
import { ApiRewardGet$Json$Params } from '../fn/reward/api-reward-get-json';
import { apiRewardGet$Plain } from '../fn/reward/api-reward-get-plain';
import { ApiRewardGet$Plain$Params } from '../fn/reward/api-reward-get-plain';
import { apiRewardPost$Json } from '../fn/reward/api-reward-post-json';
import { ApiRewardPost$Json$Params } from '../fn/reward/api-reward-post-json';
import { apiRewardPost$Plain } from '../fn/reward/api-reward-post-plain';
import { ApiRewardPost$Plain$Params } from '../fn/reward/api-reward-post-plain';
import { apiRewardRewardIdGet$Json } from '../fn/reward/api-reward-reward-id-get-json';
import { ApiRewardRewardIdGet$Json$Params } from '../fn/reward/api-reward-reward-id-get-json';
import { apiRewardRewardIdGet$Plain } from '../fn/reward/api-reward-reward-id-get-plain';
import { ApiRewardRewardIdGet$Plain$Params } from '../fn/reward/api-reward-reward-id-get-plain';
import { Reward } from '../models/reward';

@Injectable({ providedIn: 'root' })
export class RewardApiService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `apiRewardGet()` */
  static readonly ApiRewardGetPath = '/api/reward';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardGet$Plain$Response(params?: ApiRewardGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Reward>>> {
    return apiRewardGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardGet$Plain(params?: ApiRewardGet$Plain$Params, context?: HttpContext): Observable<Array<Reward>> {
    return this.apiRewardGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Reward>>): Array<Reward> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardGet$Json$Response(params?: ApiRewardGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Reward>>> {
    return apiRewardGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardGet$Json(params?: ApiRewardGet$Json$Params, context?: HttpContext): Observable<Array<Reward>> {
    return this.apiRewardGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Reward>>): Array<Reward> => r.body)
    );
  }

  /** Path part for operation `apiRewardPost()` */
  static readonly ApiRewardPostPath = '/api/reward';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardPost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiRewardPost$Plain$Response(params?: ApiRewardPost$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Reward>> {
    return apiRewardPost$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardPost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiRewardPost$Plain(params?: ApiRewardPost$Plain$Params, context?: HttpContext): Observable<Reward> {
    return this.apiRewardPost$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Reward>): Reward => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardPost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiRewardPost$Json$Response(params?: ApiRewardPost$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Reward>> {
    return apiRewardPost$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardPost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiRewardPost$Json(params?: ApiRewardPost$Json$Params, context?: HttpContext): Observable<Reward> {
    return this.apiRewardPost$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Reward>): Reward => r.body)
    );
  }

  /** Path part for operation `apiRewardRewardIdGet()` */
  static readonly ApiRewardRewardIdGetPath = '/api/reward/{rewardId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardRewardIdGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardRewardIdGet$Plain$Response(params: ApiRewardRewardIdGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Reward>> {
    return apiRewardRewardIdGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardRewardIdGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardRewardIdGet$Plain(params: ApiRewardRewardIdGet$Plain$Params, context?: HttpContext): Observable<Reward> {
    return this.apiRewardRewardIdGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Reward>): Reward => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardRewardIdGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardRewardIdGet$Json$Response(params: ApiRewardRewardIdGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Reward>> {
    return apiRewardRewardIdGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardRewardIdGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardRewardIdGet$Json(params: ApiRewardRewardIdGet$Json$Params, context?: HttpContext): Observable<Reward> {
    return this.apiRewardRewardIdGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Reward>): Reward => r.body)
    );
  }

  /** Path part for operation `apiRewardChildChildIdGet()` */
  static readonly ApiRewardChildChildIdGetPath = '/api/reward/child/{childId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardChildChildIdGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdGet$Plain$Response(params: ApiRewardChildChildIdGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Reward>>> {
    return apiRewardChildChildIdGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardChildChildIdGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdGet$Plain(params: ApiRewardChildChildIdGet$Plain$Params, context?: HttpContext): Observable<Array<Reward>> {
    return this.apiRewardChildChildIdGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Reward>>): Array<Reward> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardChildChildIdGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdGet$Json$Response(params: ApiRewardChildChildIdGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Reward>>> {
    return apiRewardChildChildIdGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardChildChildIdGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdGet$Json(params: ApiRewardChildChildIdGet$Json$Params, context?: HttpContext): Observable<Array<Reward>> {
    return this.apiRewardChildChildIdGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Reward>>): Array<Reward> => r.body)
    );
  }

  /** Path part for operation `apiRewardChildChildIdTotalGet()` */
  static readonly ApiRewardChildChildIdTotalGetPath = '/api/reward/child/{childId}/total';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardChildChildIdTotalGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdTotalGet$Plain$Response(params: ApiRewardChildChildIdTotalGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<number>> {
    return apiRewardChildChildIdTotalGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardChildChildIdTotalGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdTotalGet$Plain(params: ApiRewardChildChildIdTotalGet$Plain$Params, context?: HttpContext): Observable<number> {
    return this.apiRewardChildChildIdTotalGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<number>): number => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRewardChildChildIdTotalGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdTotalGet$Json$Response(params: ApiRewardChildChildIdTotalGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<number>> {
    return apiRewardChildChildIdTotalGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRewardChildChildIdTotalGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRewardChildChildIdTotalGet$Json(params: ApiRewardChildChildIdTotalGet$Json$Params, context?: HttpContext): Observable<number> {
    return this.apiRewardChildChildIdTotalGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<number>): number => r.body)
    );
  }

}
