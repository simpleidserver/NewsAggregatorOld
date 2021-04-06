import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
let MaterialModule = class MaterialModule {
};
MaterialModule = __decorate([
    NgModule({
        exports: [
            MatToolbarModule,
            MatSidenavModule,
            MatListModule,
            MatTableModule,
            MatCheckboxModule
        ]
    })
], MaterialModule);
export { MaterialModule };
//# sourceMappingURL=material.module.js.map