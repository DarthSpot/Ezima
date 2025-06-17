import {inject, Injectable} from '@angular/core';
import {RewardActivityApiService} from '../shared/api/services';
import {RewardActivity} from '../shared/api/models/reward-activity';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RewardActivityService {
  apiService =  inject(RewardActivityApiService);


  addActivity(newActivity: RewardActivity): Observable<RewardActivity> {
    return this.apiService.apiActivityPost$Json({
      body: newActivity
    })
  }
}
