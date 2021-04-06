import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@app/shared/material.module';
import { SharedModule } from '../shared/shared.module';
import { FeedRoutes } from './feed.routes';
import { AddFeedDialog } from './list/add-feed.component';
import { FeedListComponent } from './list/feed-list.component';

@NgModule({
  declarations: [
    AddFeedDialog,
    FeedListComponent
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
