import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildDetails } from './child-details';

describe('ChildDetails', () => {
  let component: ChildDetails;
  let fixture: ComponentFixture<ChildDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChildDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
