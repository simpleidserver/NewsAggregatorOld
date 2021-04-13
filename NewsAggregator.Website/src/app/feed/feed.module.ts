import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@app/shared/material.module';
import { DatasourceSelectorComponent } from '../common/datasourceselector/datasourceselector.component';
import { SharedModule } from '../shared/shared.module';
import { DatasourceViewArticlesComponent } from './datasource/view-datasource-articles.component';
import { DatasourceViewComponent } from './datasource/view-datasource.component';
import { FeedRoutes } from './feed.routes';
import { AddFeedDialog } from './list/add-feed.component';
import { FeedListComponent } from './list/feed-list.component';
import { FeedViewArticlesComponent } from './view/view-feed-articles.component';
import { FeedViewComponent } from './view/view-feed.component';

@NgModule({
  declarations: [
    AddFeedDialog,
    FeedListComponent,
    FeedViewComponent,
    DatasourceSelectorComponent,
    FeedViewArticlesComponent,
    DatasourceViewComponent,
    DatasourceViewArticlesComponent
  ],
  entryComponents: [
    AddFeedDialog
  ],
  imports: [
    FeedRoutes,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ]
})
export class FeedModule { }
