import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './components/404/404.component';

const routes: Routes = [
  { path: '404', component: NotFoundComponent }
];

export const StatusRoute = RouterModule.forChild(routes);
