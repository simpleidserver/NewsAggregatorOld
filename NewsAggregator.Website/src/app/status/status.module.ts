import { NgModule } from '@angular/core';
import { NotFoundComponent } from './components/404/404.component';
import { StatusRoute } from './status.routes';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    StatusRoute,
    SharedModule
  ],
  declarations: [
    NotFoundComponent
  ]
})

export class StatusModule { }
