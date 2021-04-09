import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTreeModule } from '@angular/material/tree';
import { MatCardModule } from '@angular/material/card';
let MaterialModule = class MaterialModule {
};
MaterialModule = __decorate([
    NgModule({
        exports: [
            MatToolbarModule,
            MatSidenavModule,
            MatListModule,
            MatTableModule,
            MatCheckboxModule,
            MatDialogModule,
            MatButtonModule,
            MatSelectModule,
            MatFormFieldModule,
            MatInputModule,
            MatIconModule,
            MatAutocompleteModule,
            MatTreeModule,
            MatCardModule
        ]
    })
], MaterialModule);
export { MaterialModule };
//# sourceMappingURL=material.module.js.map