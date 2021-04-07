import { RouterModule, Routes } from '@angular/router';
import { FeedListComponent } from './list/feed-list.component';
import { FeedViewComponent } from './view/view-feed.component';

const routes: Routes = [
  { path: '', component: FeedListComponent },
  { path: ':id', component: FeedViewComponent }
];

export const FeedRoutes = RouterModule.forChild(routes);
