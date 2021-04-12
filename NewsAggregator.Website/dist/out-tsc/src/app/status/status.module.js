import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { NotFoundComponent } from './components/404/404.component';
import { StatusRoute } from './status.routes';
import { SharedModule } from '@app/shared/shared.module';
let StatusModule = class StatusModule {
};
StatusModule = __decorate([
    NgModule({
        imports: [
            StatusRoute,
            SharedModule
        ],
        declarations: [
            NotFoundComponent
        ]
    })
], StatusModule);
export { StatusModule };
//# sourceMappingURL=status.module.js.map