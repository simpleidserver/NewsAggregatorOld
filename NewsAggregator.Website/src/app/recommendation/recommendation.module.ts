import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@app/shared/material.module';
import { SharedModule } from '../shared/shared.module';
import { ListRecommendationComponent } from './list/list-recommendation.component';
import { RecommendationViewArticlesComponent } from './list/view-recommendation-articles.component';
import { RecommendationRoutes } from './recommendation.routes';

@NgModule({
  declarations: [
    RecommendationViewArticlesComponent,
    ListRecommendationComponent
  ],
  imports: [
    RecommendationRoutes,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ]
})
export class RecommendationModule { }
