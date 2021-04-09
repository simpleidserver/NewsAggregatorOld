import { RouterModule } from '@angular/router';
import { FeedListComponent } from './list/feed-list.component';
import { FeedViewComponent } from './view/view-feed.component';
const routes = [
    { path: '', component: FeedListComponent },
    { path: ':id', component: FeedViewComponent }
];
export const FeedRoutes = RouterModule.forChild(routes);
//# sourceMappingURL=feed.routes.js.map