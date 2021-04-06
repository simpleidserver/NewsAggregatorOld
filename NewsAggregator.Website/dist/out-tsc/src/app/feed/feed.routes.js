import { RouterModule } from '@angular/router';
import { FeedListComponent } from './list/feed-list.component';
const routes = [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', component: FeedListComponent }
];
export const FeedRoutes = RouterModule.forChild(routes);
//# sourceMappingURL=feed.routes.js.map