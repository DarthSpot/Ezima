import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildrenOverview } from './children-overview';

describe('ChildrenOverview', () => {
  let component: ChildrenOverview;
  let fixture: ComponentFixture<ChildrenOverview>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChildrenOverview]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildrenOverview);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
