import { RouterModule, Routes } from '@angular/router';
import { ListRecommendationComponent } from './list/list-recommendation.component';

const routes: Routes = [
  { path: '', component: ListRecommendationComponent }
];

export const RecommendationRoutes = RouterModule.forChild(routes);
