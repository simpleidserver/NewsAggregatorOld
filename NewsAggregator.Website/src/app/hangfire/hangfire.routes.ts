import { RouterModule, Routes } from '@angular/router';
import { HangfireJobsComponent } from './list/hangfire-jobs.component';

const routes: Routes = [
  { path: '', component: HangfireJobsComponent }
];

export const HangfireRoutes = RouterModule.forChild(routes);
