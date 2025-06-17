import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserProfileInfo } from './user-profile-info';

describe('UserInfo', () => {
  let component: UserProfileInfo;
  let fixture: ComponentFixture<UserProfileInfo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserProfileInfo]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserProfileInfo);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
