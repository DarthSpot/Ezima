import {Component, inject, OnInit} from '@angular/core';
import {Child} from '../../shared/api/models/child';
import {ChildService} from '../../service/child-service';
import {ChildInfo} from "../../shared/api/models/child-info";

@Component({
  selector: 'app-children-overview',
  standalone: false,
  templateUrl: './children-overview.html',
  styleUrl: './children-overview.css'
})
export class ChildrenOverview implements OnInit {
  childService = inject(ChildService);
  children: ChildInfo[] = [];

  ngOnInit(): void {
    this.childService.getChildren()
      .subscribe(children => this.children = children);
  }

  add(name: string, birthday: string): void {
    name = name.trim();
    if (!name)
      return;
    this.childService.addChild({
      name: name,
      birthday: birthday
    }).subscribe(res => {
      this.children.push({
        id: res.id,
        name: res.name,
        currentRewardTime: 0
      })
    });
  }
}
