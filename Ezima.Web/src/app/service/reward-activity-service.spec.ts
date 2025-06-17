import { TestBed } from '@angular/core/testing';

import { RewardActivityService } from './reward-activity-service';

describe('RewardActivityService', () => {
  let service: RewardActivityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RewardActivityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
