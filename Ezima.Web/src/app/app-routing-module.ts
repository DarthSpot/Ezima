import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {Dashboard} from './components/dashboard/dashboard';
import {ChildrenOverview} from './components/children-overview/children-overview';
import {RewardOverview} from './components/reward-overview/reward-overview';
import {UsageOverview} from './components/usage-overview/usage-overview';
import {ActivityOverview} from './components/activity-overview/activity-overview';
import {LoginSuccess} from './components/login-success/login-success';
import {MainShell} from './components/main/main-shell';
import {PageNotFound} from './components/page-not-found/page-not-found';
import {Welcome} from './components/welcome/welcome';
import {ChildDetails} from "./components/child-details/child-details";

const routes: Routes = [
  { path: '', component: MainShell,
    children: [
      { path: 'welcome', component: Welcome},
      { path: 'dashboard', component: Dashboard },
      { path: 'children', component: ChildrenOverview },
      { path: 'children/:id', component: ChildDetails },
      { path: 'rewards', component: RewardOverview },
      { path: 'usage', component: UsageOverview },
      { path: 'activity', component: ActivityOverview },
    ]},
  { path: 'set-token', component: LoginSuccess },
  { path: '**', component: PageNotFound },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
