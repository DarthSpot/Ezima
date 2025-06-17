import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardActivityDetails } from './reward-activity-details';

describe('RewardActivityDetails', () => {
  let component: RewardActivityDetails;
  let fixture: ComponentFixture<RewardActivityDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardActivityDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RewardActivityDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
