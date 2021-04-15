import { RouterModule, Routes } from '@angular/router';
import { HangfireJobsComponent } from './list/hangfire-jobs.component';
import { HangfireJobStatesComponent } from './view/hangfire-jobstates.component';

const routes: Routes = [
  { path: '', component: HangfireJobsComponent },
  { path: ':id', component: HangfireJobStatesComponent }
];

export const HangfireRoutes = RouterModule.forChild(routes);
