import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@app/shared/material.module';
import { SharedModule } from '../shared/shared.module';
import { HangfireRoutes } from './hangfire.routes';
import { HangfireJobsComponent } from './list/hangfire-jobs.component';
import { HangfireJobStatesComponent } from './view/hangfire-jobstates.component';

@NgModule({
  declarations: [
    HangfireJobsComponent,
    HangfireJobStatesComponent
  ],
  entryComponents: [
  ],
  imports: [
    HangfireRoutes,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ]
})
export class HangfireModule { }
