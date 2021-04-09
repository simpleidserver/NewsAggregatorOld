import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './components/404/404.component';
const routes = [
    { path: '404', component: NotFoundComponent },
    { path: '401', component: UnauthorizedComponent }
];
export const StatusRoute = RouterModule.forChild(routes);
//# sourceMappingURL=status.routes.js.map