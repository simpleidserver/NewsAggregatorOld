import { RouterModule, Routes } from '@angular/router';
import { DatasourceViewComponent } from './datasource/view-datasource.component';
import { FeedListComponent } from './list/feed-list.component';
import { FeedViewComponent } from './view/view-feed.component';

const routes: Routes = [
  { path: '', component: FeedListComponent },
  { path: ':id', component: FeedViewComponent },
  { path: ':id/datasources/:datasourceid', component: DatasourceViewComponent }
];

export const FeedRoutes = RouterModule.forChild(routes);
