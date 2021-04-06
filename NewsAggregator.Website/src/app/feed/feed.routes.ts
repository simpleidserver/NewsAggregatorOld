import { RouterModule, Routes } from '@angular/router';
import { FeedListComponent } from './list/feed-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { path: 'list', component: FeedListComponent }
];

export const FeedRoutes = RouterModule.forChild(routes);
