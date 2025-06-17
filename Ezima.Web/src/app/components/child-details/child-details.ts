import {Component, inject, OnInit} from '@angular/core';
import {Child} from '../../shared/api/models/child';
import {ActivatedRoute} from '@angular/router';
import { Location } from '@angular/common';
import {ChildService} from '../../service/child-service';

@Component({
  selector: 'app-child-details',
  standalone: false,
  templateUrl: './child-details.html',
  styleUrl: './child-details.css'
})
export class ChildDetails implements OnInit {
  childService = inject(ChildService);
  child?: Child;

  constructor(private route: ActivatedRoute,
              private location: Location) {
  }

  ngOnInit(): void {
    this.getChild();
  }

  goBack(): void {
    this.location.back();
  }

  private getChild() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.childService.getChild(id)
      .subscribe(child => this.child = child);
  }

  protected readonly Date = Date;
}
