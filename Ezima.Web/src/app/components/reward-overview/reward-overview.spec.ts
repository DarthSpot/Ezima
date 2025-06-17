import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardOverview } from './reward-overview';

describe('RewardOverview', () => {
  let component: RewardOverview;
  let fixture: ComponentFixture<RewardOverview>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardOverview]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RewardOverview);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
