import {Component, inject} from '@angular/core';
import {RewardActivity} from '../../shared/api/models/reward-activity';
import {RewardActivityService} from '../../service/reward-activity-service';

@Component({
  selector: 'app-activity-overview',
  standalone: false,
  templateUrl: './activity-overview.html',
  styleUrl: './activity-overview.css'
})
export class ActivityOverview {
  activities: RewardActivity[] = [];
  activityService = inject(RewardActivityService);

  add(name: string, defaultTime: string, description?: string) {
    name = name.trim();
    const defaultMinutes = Number(defaultTime.trim());
    if (!name || !defaultTime) {
      return;
    }
    this.activityService.addActivity({
      name: name,
      defaultMinutes: defaultMinutes,
      description: description
    })
      .subscribe(result => {
        this.activities.push(result);
      });
  }
}
