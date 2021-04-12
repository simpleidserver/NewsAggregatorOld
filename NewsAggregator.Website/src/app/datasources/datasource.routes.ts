import { RouterModule, Routes } from '@angular/router';
import { DatasourceViewComponent } from './view/view-datasource.component';

const routes: Routes = [
  { path: ':id', component: DatasourceViewComponent }
];

export const DatasourceRoutes = RouterModule.forChild(routes);
