import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@app/shared/material.module';
import { SharedModule } from '../shared/shared.module';
import { DatasourceRoutes } from './datasource.routes';
import { DatasourceViewArticlesComponent } from './view/view-datasource-articles.component';
import { DatasourceViewComponent } from './view/view-datasource.component';

@NgModule({
  declarations: [
    DatasourceViewArticlesComponent,
    DatasourceViewComponent
  ],
  entryComponents: [
  ],
  imports: [
    DatasourceRoutes,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ]
})
export class DatasourceModule { }
