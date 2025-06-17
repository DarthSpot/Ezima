import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardDetails } from './reward-details';

describe('RewardDetails', () => {
  let component: RewardDetails;
  let fixture: ComponentFixture<RewardDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RewardDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
