import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { FeedRoutes } from './feed.routes';
import { FeedListComponent } from './list/feed-list.component';
import { MaterialModule } from '@app/shared/material.module';
let FeedModule = class FeedModule {
};
FeedModule = __decorate([
    NgModule({
        declarations: [
            FeedListComponent
        ],
        imports: [
            FeedRoutes,
            MaterialModule
        ]
    })
], FeedModule);
export { FeedModule };
//# sourceMappingURL=feed.module.js.map