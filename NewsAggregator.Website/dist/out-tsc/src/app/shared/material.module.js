import { __decorate } from "tslib";
import { OverlayModule } from '@angular/cdk/overlay';
import { NgModule } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTreeModule } from '@angular/material/tree';
import { MatSnackBarModule } from '@angular/material/snack-bar';
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
            MatCardModule,
            MatProgressSpinnerModule,
            OverlayModule,
            MatSnackBarModule
        ]
    })
], MaterialModule);
export { MaterialModule };
//# sourceMappingURL=material.module.js.map