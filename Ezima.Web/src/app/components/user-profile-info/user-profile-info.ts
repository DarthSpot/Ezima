import {Component, inject, OnInit} from '@angular/core';
import {UserInfo} from '../../shared/api/models/user-info';
import {UserService} from '../../service/user-service';

@Component({
  selector: 'app-user-profile-info',
  standalone: false,
  templateUrl: './user-profile-info.html',
  styleUrl: './user-profile-info.css'
})
export class UserProfileInfo implements OnInit {
  public userInfo: UserInfo | null = null;
  private userService = inject(UserService)
  ngOnInit(): void {
    this.userService.getUserInfo().subscribe(data => {
      this.userInfo = data;
    })
  }

}
