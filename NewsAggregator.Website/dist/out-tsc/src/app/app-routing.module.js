import { __awaiter, __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './infrastructures/auth-guard.service';
const routes = [
    { path: '', redirectTo: 'feeds', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    {
        path: 'feeds',
        loadChildren: () => __awaiter(void 0, void 0, void 0, function* () { return (yield import('./feed/feed.module')).FeedModule; }),
        canActivate: [AuthGuard]
    },
    {
        path: 'status',
        loadChildren: () => __awaiter(void 0, void 0, void 0, function* () { return (yield import('./status/status.module')).StatusModule; })
    }
];
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = __decorate([
    NgModule({
        imports: [RouterModule.forRoot(routes)],
        exports: [RouterModule]
    })
], AppRoutingModule);
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map