import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'feeds', pathMatch: 'full' },
  {
    path: 'feeds',
    loadChildren: async () => (await import('./feed/feed.module')).FeedModule
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
