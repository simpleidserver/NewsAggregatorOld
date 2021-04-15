import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './infrastructures/auth-guard.service';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  {
    path: 'feeds',
    loadChildren: async () => (await import('./feed/feed.module')).FeedModule,
    canActivate: [ AuthGuard ]
  },
  {
    path: 'status',
    loadChildren: async () => (await import('./status/status.module')).StatusModule
  },
  {
    path: 'hangfire',
    loadChildren: async () => (await import('./hangfire/hangfire.module')).HangfireModule
  },
  {
    path: 'recommendations',
    loadChildren: async () => (await import('./recommendation/recommendation.module')).RecommendationModule
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
