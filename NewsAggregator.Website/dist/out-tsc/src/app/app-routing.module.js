import { __awaiter, __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
const routes = [
    { path: '', redirectTo: 'feeds', pathMatch: 'full' },
    {
        path: 'feeds',
        loadChildren: () => __awaiter(void 0, void 0, void 0, function* () { return (yield import('./feed/feed.module')).FeedModule; })
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